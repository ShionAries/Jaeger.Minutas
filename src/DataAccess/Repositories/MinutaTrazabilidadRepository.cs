using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using Jaeger.Minutas.DataAccess.Entities;
using Jaeger.Minutas.DataAccess.Mapping;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.DataAccess.Repositories {
    public sealed class MinutaTrazabilidadRepository : IMinutaTrazabilidadRepository {
        private readonly SqlSugarClient _db;

        public MinutaTrazabilidadRepository(SqlSugarClient db) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<List<MinutaSeguimientoPendiente>> ObtenerPendientesAsync(long minutaOrigenId) {
            if (minutaOrigenId <= 0) throw new ArgumentOutOfRangeException(nameof(minutaOrigenId));
            var origen = await _db.Queryable<MinutaEntity>().Where(x => x.Id == minutaOrigenId && !x.Eliminado).FirstAsync().ConfigureAwait(false);
            if (origen == null) throw new InvalidOperationException("La minuta de origen no existe o está eliminada.");
            var tareas = await _db.Queryable<MinutaTareaEntity>().Where(x => x.MinutaId == minutaOrigenId).ToListAsync().ConfigureAwait(false);
            var acuerdos = await _db.Queryable<MinutaAcuerdoEntity>().Where(x => x.MinutaId == minutaOrigenId).ToListAsync().ConfigureAwait(false);
            var resultado = new List<MinutaSeguimientoPendiente>();
            foreach (var tarea in tareas.Where(x => RequiereSeguimiento(x.Estado))) {
                var acuerdo = tarea.AcuerdoId.HasValue ? acuerdos.FirstOrDefault(x => x.Id == tarea.AcuerdoId.Value) : null;
                resultado.Add(new MinutaSeguimientoPendiente {
                    TareaId = tarea.Id, AcuerdoId = tarea.AcuerdoId,
                    Acuerdo = acuerdo == null ? "(Sin acuerdo asociado)" : acuerdo.Descripcion,
                    Descripcion = tarea.Descripcion, ResponsableId = tarea.ResponsableId,
                    FechaLimite = tarea.FechaLimite, Estado = tarea.Estado, Seleccionar = true
                });
            }
            return resultado.OrderBy(x => x.FechaLimite).ThenBy(x => x.TareaId).ToList();
        }

        public async Task<long> CrearSeguimientoAsync(long minutaOrigenId, Minuta nuevaMinuta, IList<long> tareasOrigenIds, long usuarioId) {
            if (minutaOrigenId <= 0) throw new ArgumentOutOfRangeException(nameof(minutaOrigenId));
            if (nuevaMinuta == null) throw new ArgumentNullException(nameof(nuevaMinuta));
            if (usuarioId <= 0) throw new ArgumentOutOfRangeException(nameof(usuarioId));
            var seleccionadas = (tareasOrigenIds ?? new List<long>()).Distinct().ToList();
            var usuarioActivo = await _db.Queryable<UsuarioCorporativoEntity>().AnyAsync(x => x.Id == usuarioId && x.Activo == 1).ConfigureAwait(false);
            if (!usuarioActivo) throw new InvalidOperationException("El usuario de la sesión no existe o está inactivo.");

            var origen = await _db.Queryable<MinutaEntity>().Where(x => x.Id == minutaOrigenId && !x.Eliminado).FirstAsync().ConfigureAwait(false);
            if (origen == null) throw new InvalidOperationException("La minuta de origen no existe o está eliminada.");
            if (!string.Equals(origen.Estado, EstadoMinuta.Aprobada.ToString(), StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Solo se puede crear seguimiento a partir de una minuta aprobada. La minuta de origen no se modificará.");

            var tareasOrigen = await _db.Queryable<MinutaTareaEntity>().Where(x => x.MinutaId == minutaOrigenId).ToListAsync().ConfigureAwait(false);
            var tareasSeleccionadas = tareasOrigen.Where(x => seleccionadas.Contains(x.Id) && RequiereSeguimiento(x.Estado)).ToList();
            if (tareasSeleccionadas.Count != seleccionadas.Count)
                throw new InvalidOperationException("Una o más tareas seleccionadas ya no existen o están concluidas. Actualice la lista e inténtelo nuevamente.");

            var acuerdosOrigen = await _db.Queryable<MinutaAcuerdoEntity>().Where(x => x.MinutaId == minutaOrigenId).ToListAsync().ConfigureAwait(false);
            var asistentesOrigen = await _db.Queryable<MinutaAsistenteEntity>().Where(x => x.MinutaId == minutaOrigenId).ToListAsync().ConfigureAwait(false);
            var entidadNueva = MinutaMapper.ToEntity(nuevaMinuta, usuarioId);
            entidadNueva.Folio = "TMP-" + Guid.NewGuid().ToString("N").Substring(0, 24);

            _db.Ado.BeginTran();
            try {
                long idNueva = await _db.Insertable(entidadNueva).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                string folio = "MIN-" + idNueva.ToString("D6");
                int filas = await _db.Ado.ExecuteCommandAsync("UPDATE MNTA SET MNTA_FOLIO=@folio WHERE MNTA_ID=@id",
                    new SugarParameter("@folio", folio), new SugarParameter("@id", idNueva)).ConfigureAwait(false);
                if (filas != 1) throw new InvalidOperationException("No fue posible asignar el folio automático a la minuta de seguimiento.");
                entidadNueva.Id = idNueva; entidadNueva.Folio = folio;

                long relacionId = await _db.Insertable(new MinutaRelacionEntity {
                    MinutaOrigenId = minutaOrigenId, MinutaDestinoId = idNueva, UsuarioId = usuarioId, FechaCreacion = DateTime.Now
                }).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);

                foreach (var asistente in asistentesOrigen) {
                    await _db.Insertable(new MinutaAsistenteEntity {
                        MinutaId = idNueva, UsuarioId = asistente.UsuarioId, Nombre = asistente.Nombre,
                        Correo = asistente.Correo, Rol = asistente.Rol, EstadoAsistencia = "Convocado"
                    }).ExecuteCommandAsync().ConfigureAwait(false);
                }

                var acuerdoDestinoPorOrigen = new Dictionary<long, long>();
                int ordenSinAcuerdo = acuerdosOrigen.Count == 0 ? 0 : acuerdosOrigen.Max(x => x.Orden);
                foreach (var tareaOrigen in tareasSeleccionadas) {
                    long? acuerdoDestinoId = null;
                    if (tareaOrigen.AcuerdoId.HasValue) {
                        var acuerdoOrigen = acuerdosOrigen.FirstOrDefault(x => x.Id == tareaOrigen.AcuerdoId.Value);
                        if (acuerdoOrigen != null) {
                            if (!acuerdoDestinoPorOrigen.TryGetValue(acuerdoOrigen.Id, out long encontrado)) {
                                var acuerdoNuevo = new MinutaAcuerdoEntity {
                                    MinutaId = idNueva, TemaId = null, Tipo = acuerdoOrigen.Tipo,
                                    Descripcion = acuerdoOrigen.Descripcion, Orden = ++ordenSinAcuerdo,
                                    RegistradoPor = usuarioId, FechaRegistro = DateTime.Now
                                };
                                encontrado = await _db.Insertable(acuerdoNuevo).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                                acuerdoDestinoPorOrigen.Add(acuerdoOrigen.Id, encontrado);
                            }
                            acuerdoDestinoId = encontrado;
                        }
                    }
                    var tareaNueva = new MinutaTareaEntity {
                        MinutaId = idNueva, AcuerdoId = acuerdoDestinoId, Descripcion = tareaOrigen.Descripcion,
                        ResponsableId = tareaOrigen.ResponsableId, FechaLimite = tareaOrigen.FechaLimite,
                        Estado = "Pendiente", FechaFinalizacion = null, Evidencia = null, CreadoPor = usuarioId, FechaCreacion = DateTime.Now
                    };
                    long tareaDestinoId = await _db.Insertable(tareaNueva).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                    await _db.Insertable(new MinutaTrazabilidadTareaEntity {
                        RelacionId = relacionId, TareaOrigenId = tareaOrigen.Id, TareaDestinoId = tareaDestinoId,
                        AcuerdoOrigenId = tareaOrigen.AcuerdoId, AcuerdoDestinoId = acuerdoDestinoId,
                        TareaOrigenDescripcion = tareaOrigen.Descripcion,
                        TareaDestinoDescripcion = tareaOrigen.Descripcion,
                        AcuerdoOrigenDescripcion = tareaOrigen.AcuerdoId.HasValue ? (acuerdosOrigen.FirstOrDefault(x => x.Id == tareaOrigen.AcuerdoId.Value)?.Descripcion) : null,
                        AcuerdoDestinoDescripcion = acuerdoDestinoId.HasValue ? (acuerdosOrigen.FirstOrDefault(x => x.Id == tareaOrigen.AcuerdoId.Value)?.Descripcion) : null
                    }).ExecuteCommandAsync().ConfigureAwait(false);
                }

                await MinutaAuditoriaWriter.RegistrarAsync(_db, idNueva, usuarioId, "CREAR_SEGUIMIENTO",
                    null, MinutaAuditoriaWriter.Snapshot(entidadNueva)).ConfigureAwait(false);
                await MinutaAuditoriaWriter.RegistrarAsync(_db, minutaOrigenId, usuarioId, "SEGUIMIENTO_CREADO",
                    null, "{\"MinutaDestinoId\":" + idNueva + ",\"FolioDestino\":\"" + folio + "\"}").ConfigureAwait(false);
                _db.Ado.CommitTran();
                return idNueva;
            } catch {
                _db.Ado.RollbackTran();
                throw;
            }
        }

        public async Task<List<MinutaTrazabilidadNodo>> ObtenerCadenaAsync(long minutaId) {
            if (minutaId <= 0) throw new ArgumentOutOfRangeException(nameof(minutaId));
            var minutas = await _db.Queryable<MinutaEntity>().ToListAsync().ConfigureAwait(false);
            if (!minutas.Any(x => x.Id == minutaId)) throw new InvalidOperationException("No existe la minuta seleccionada.");
            var relaciones = await _db.Queryable<MinutaRelacionEntity>().ToListAsync().ConfigureAwait(false);
            var niveles = new Dictionary<long, int> { { minutaId, 0 } };
            var cola = new Queue<long>(); cola.Enqueue(minutaId);
            while (cola.Count > 0) {
                long actual = cola.Dequeue();
                foreach (var relacion in relaciones) {
                    long vecino; int nivel;
                    if (relacion.MinutaDestinoId == actual) { vecino = relacion.MinutaOrigenId; nivel = niveles[actual] - 1; }
                    else if (relacion.MinutaOrigenId == actual) { vecino = relacion.MinutaDestinoId; nivel = niveles[actual] + 1; }
                    else continue;
                    if (!niveles.ContainsKey(vecino)) { niveles[vecino] = nivel; cola.Enqueue(vecino); }
                }
            }
            return minutas.Where(x => niveles.ContainsKey(x.Id))
                .Select(x => new MinutaTrazabilidadNodo {
                    MinutaId = x.Id, Folio = x.Folio, Titulo = x.Titulo, FechaReunion = x.FechaReunion,
                    Estado = x.Estado, Nivel = niveles[x.Id], Eliminada = x.Eliminado,
                    Relacion = niveles[x.Id] == 0 ? "Minuta seleccionada" : niveles[x.Id] < 0 ? "Antecedente" : "Continuación"
                })
                .OrderBy(x => x.Nivel).ThenBy(x => x.FechaReunion).ToList();
        }

        public async Task<List<MinutaTrazabilidadTarea>> ObtenerTareasRelacionadasAsync(long minutaId) {
            if (minutaId <= 0) throw new ArgumentOutOfRangeException(nameof(minutaId));
            var relaciones = await _db.Queryable<MinutaRelacionEntity>()
                .Where(x => x.MinutaOrigenId == minutaId || x.MinutaDestinoId == minutaId).ToListAsync().ConfigureAwait(false);
            var resultado = new List<MinutaTrazabilidadTarea>();
            foreach (var relacion in relaciones) {
                var origen = await _db.Queryable<MinutaEntity>().Where(x => x.Id == relacion.MinutaOrigenId).FirstAsync().ConfigureAwait(false);
                var destino = await _db.Queryable<MinutaEntity>().Where(x => x.Id == relacion.MinutaDestinoId).FirstAsync().ConfigureAwait(false);
                var mapas = await _db.Queryable<MinutaTrazabilidadTareaEntity>()
                    .Where(x => x.RelacionId == relacion.Id).ToListAsync().ConfigureAwait(false);
                foreach (var mapa in mapas) {
                    var tareaOrigen = await _db.Queryable<MinutaTareaEntity>().Where(x => x.Id == mapa.TareaOrigenId).FirstAsync().ConfigureAwait(false);
                    var tareaDestino = await _db.Queryable<MinutaTareaEntity>().Where(x => x.Id == mapa.TareaDestinoId).FirstAsync().ConfigureAwait(false);
                    var acuerdoOrigen = mapa.AcuerdoOrigenId.HasValue ? await _db.Queryable<MinutaAcuerdoEntity>().Where(x => x.Id == mapa.AcuerdoOrigenId.Value).FirstAsync().ConfigureAwait(false) : null;
                    var acuerdoDestino = mapa.AcuerdoDestinoId.HasValue ? await _db.Queryable<MinutaAcuerdoEntity>().Where(x => x.Id == mapa.AcuerdoDestinoId.Value).FirstAsync().ConfigureAwait(false) : null;
                    resultado.Add(new MinutaTrazabilidadTarea {
                        FolioOrigen = origen == null ? string.Empty : origen.Folio,
                        FolioDestino = destino == null ? string.Empty : destino.Folio,
                        AcuerdoOrigen = acuerdoOrigen == null ? (mapa.AcuerdoOrigenDescripcion ?? "(Sin acuerdo)") : acuerdoOrigen.Descripcion,
                        TareaOrigen = tareaOrigen == null ? mapa.TareaOrigenDescripcion : tareaOrigen.Descripcion,
                        EstadoOrigen = tareaOrigen == null ? "Registro histórico" : tareaOrigen.Estado,
                        AcuerdoDestino = acuerdoDestino == null ? (mapa.AcuerdoDestinoDescripcion ?? "(Sin acuerdo)") : acuerdoDestino.Descripcion,
                        TareaDestino = tareaDestino == null ? mapa.TareaDestinoDescripcion : tareaDestino.Descripcion,
                        EstadoDestino = tareaDestino == null ? "Registro histórico" : tareaDestino.Estado
                    });
                }
            }
            return resultado;
        }

        private static bool RequiereSeguimiento(string estado) {
            if (string.IsNullOrWhiteSpace(estado)) return true;
            string valor = estado.Trim().ToUpperInvariant();
            return !(valor == "COMPLETADA" || valor == "COMPLETADO" || valor == "CONCLUIDA" ||
                valor == "CONCLUIDO" || valor == "FINALIZADA" || valor == "FINALIZADO" ||
                valor == "CERRADA" || valor == "CERRADO" || valor == "CANCELADA" || valor == "CANCELADO" ||
                valor == "RESUELTA" || valor == "RESUELTO" || valor == "TERMINADA" || valor == "TERMINADO");
        }
    }
}
