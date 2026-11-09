using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;
using Jaeger.Minutas.DataAccess.Entities;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Exceptions;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.DataAccess.Repositories {
    public sealed class MinutaDetalleRepository : IMinutaDetalleRepository {
        private readonly SqlSugarClient _db;
        public MinutaDetalleRepository(SqlSugarClient db) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<MinutaDetalle> ObtenerAsync(long minutaId) {
            var minuta = await _db.Queryable<MinutaEntity>()
                .Where(x => x.Id == minutaId && !x.Eliminado)
                .FirstAsync().ConfigureAwait(false);
            if (minuta == null)
                throw new InvalidOperationException("La minuta indicada no existe o está eliminada.");

            var result = new MinutaDetalle { MinutaId = minutaId, VersionFila = minuta.VersionFila };
            var asistentes = await _db.Queryable<MinutaAsistenteEntity>().Where(x => x.MinutaId == minutaId).ToListAsync().ConfigureAwait(false);
            var temas = await _db.Queryable<MinutaTemaEntity>().Where(x => x.MinutaId == minutaId).OrderBy(x => x.Orden).ToListAsync().ConfigureAwait(false);
            var acuerdos = await _db.Queryable<MinutaAcuerdoEntity>().Where(x => x.MinutaId == minutaId).OrderBy(x => x.Orden).ToListAsync().ConfigureAwait(false);
            var tareas = await _db.Queryable<MinutaTareaEntity>().Where(x => x.MinutaId == minutaId).ToListAsync().ConfigureAwait(false);

            result.Asistentes = asistentes.Select(x => new MinutaAsistente { Id = x.Id, IdMinuta = x.MinutaId, IdUsuario = x.UsuarioId, Nombre = x.Nombre, Correo = x.Correo, Rol = x.Rol, EstadoAsistencia = x.EstadoAsistencia }).ToList();
            result.Temas = temas.Select(x => new MinutaTema { Id = x.Id, MinutaId = x.MinutaId, Orden = x.Orden, Tema = x.Tema, Descripcion = x.Descripcion, ResponsableId = x.ResponsableId, Estado = x.Estado }).ToList();
            result.Acuerdos = acuerdos.Select(x => new MinutaAcuerdo { Id = x.Id, MinutaId = x.MinutaId, TemaId = x.TemaId, Tipo = x.Tipo, Descripcion = x.Descripcion, Orden = x.Orden, RegistradoPor = x.RegistradoPor, FechaRegistro = x.FechaRegistro }).ToList();
            result.Tareas = tareas.Select(x => new MinutaTarea { Id = x.Id, MinutaId = x.MinutaId, AcuerdoId = x.AcuerdoId, Descripcion = x.Descripcion, ResponsableId = x.ResponsableId, FechaLimite = x.FechaLimite, Estado = x.Estado, FechaFinalizacion = x.FechaFinalizacion, Evidencia = x.Evidencia, CreadoPor = x.CreadoPor }).ToList();
            foreach (var acuerdo in result.Acuerdos) {
                var tema = result.Temas.FirstOrDefault(x => x.Id == acuerdo.TemaId);
                if (tema != null)
                    acuerdo.TemaOrdenSeleccionado = tema.Orden;
            }
            foreach (var tarea in result.Tareas) {
                var acuerdo = result.Acuerdos.FirstOrDefault(x => x.Id == tarea.AcuerdoId);
                if (acuerdo != null)
                    tarea.AcuerdoOrdenSeleccionado = acuerdo.Orden;
            }
            return result;
        }

        public async Task GuardarAsync(MinutaDetalle detalle, long usuarioId) {
            bool usuarioActivo = await _db.Queryable<UsuarioCorporativoEntity>().AnyAsync(x => x.Id == usuarioId && x.Activo == 1).ConfigureAwait(false);
            if (!usuarioActivo)
                throw new InvalidOperationException("El usuario de la sesión no existe o está inactivo en el catálogo corporativo.");

            var responsables = detalle.Temas.Where(x => x.ResponsableId.HasValue).Select(x => x.ResponsableId.Value)
                .Concat(detalle.Tareas.Select(x => x.ResponsableId)).Distinct().ToList();
            foreach (long id in responsables) {
                if (!await _db.Queryable<UsuarioCorporativoEntity>().AnyAsync(x => x.Id == id && x.Activo == 1).ConfigureAwait(false))
                    throw new InvalidOperationException("Uno de los responsables seleccionados no existe o está inactivo en el catálogo corporativo.");
            }
            foreach (long id in detalle.Asistentes.Where(x => x.IdUsuario.HasValue).Select(x => x.IdUsuario.Value).Distinct()) {
                if (!await _db.Queryable<UsuarioCorporativoEntity>().AnyAsync(x => x.Id == id && x.Activo == 1).ConfigureAwait(false))
                    throw new InvalidOperationException("Uno de los asistentes seleccionados no existe o está inactivo en el catálogo corporativo.");
            }
            if (detalle.VersionFila <= 0)
                throw new InvalidOperationException("No se obtuvo la versión de la minuta. Vuelva a cargarla antes de guardar.");

            _db.Ado.BeginTran();
            try {
                int actualizadas = await _db.Ado.ExecuteCommandAsync(
                    "UPDATE MNTA SET MNTA_VERFIL = MNTA_VERFIL + 1, MNTA_FCACT = CURRENT_TIMESTAMP WHERE MNTA_ID = @id AND MNTA_VERFIL = @version AND MNTA_A = 0 AND MNTA_STTS_ID NOT IN ('Aprobada', 'Cancelada')",
                    new SugarParameter("@id", detalle.MinutaId), new SugarParameter("@version", detalle.VersionFila)).ConfigureAwait(false);
                if (actualizadas != 1)
                    throw new MinutaConcurrenteException();

                var asistentesActuales = await _db.Queryable<MinutaAsistenteEntity>().Where(x => x.MinutaId == detalle.MinutaId).ToListAsync().ConfigureAwait(false);
                var tareasActuales = await _db.Queryable<MinutaTareaEntity>().Where(x => x.MinutaId == detalle.MinutaId).ToListAsync().ConfigureAwait(false);
                var acuerdosActuales = await _db.Queryable<MinutaAcuerdoEntity>().Where(x => x.MinutaId == detalle.MinutaId).ToListAsync().ConfigureAwait(false);
                var temasActuales = await _db.Queryable<MinutaTemaEntity>().Where(x => x.MinutaId == detalle.MinutaId).ToListAsync().ConfigureAwait(false);

                var idsTareasActuales = new HashSet<long>(tareasActuales.Select(x => x.Id));
                var idsAcuerdosActuales = new HashSet<long>(acuerdosActuales.Select(x => x.Id));
                var idsTemasActuales = new HashSet<long>(temasActuales.Select(x => x.Id));
                var idsAsistentesActuales = new HashSet<long>(asistentesActuales.Select(x => x.Id));
                if (detalle.Tareas.Any(x => x.Id > 0 && !idsTareasActuales.Contains(x.Id)) ||
                    detalle.Acuerdos.Any(x => x.Id > 0 && !idsAcuerdosActuales.Contains(x.Id)) ||
                    detalle.Temas.Any(x => x.Id > 0 && !idsTemasActuales.Contains(x.Id)) ||
                    detalle.Asistentes.Any(x => x.Id > 0 && !idsAsistentesActuales.Contains(x.Id)))
                    throw new InvalidOperationException("El detalle contiene identificadores que no pertenecen a esta minuta. Recargue la información e inténtelo nuevamente.");

                var idsTareas = new HashSet<long>(detalle.Tareas.Where(x => x.Id > 0).Select(x => x.Id));
                var idsAcuerdos = new HashSet<long>(detalle.Acuerdos.Where(x => x.Id > 0).Select(x => x.Id));
                var idsTemas = new HashSet<long>(detalle.Temas.Where(x => x.Id > 0).Select(x => x.Id));

                foreach (var a in acuerdosActuales.Where(x => !idsAcuerdos.Contains(x.Id)))
                    await _db.Ado.ExecuteCommandAsync("UPDATE MNTTA SET MNTTA_MNTAC_ID = NULL WHERE MNTTA_MNTA_ID = @minutaId AND MNTTA_MNTAC_ID = @acuerdoId",
                        new SugarParameter("@minutaId", detalle.MinutaId), new SugarParameter("@acuerdoId", a.Id)).ConfigureAwait(false);
                foreach (var t in temasActuales.Where(x => !idsTemas.Contains(x.Id)))
                    await _db.Ado.ExecuteCommandAsync("UPDATE MNTAC SET MNTAC_TEMA_ID = NULL WHERE MNTAC_MNTA_ID = @minutaId AND MNTAC_TEMA_ID = @temaId",
                        new SugarParameter("@minutaId", detalle.MinutaId), new SugarParameter("@temaId", t.Id)).ConfigureAwait(false);

                var idsTareasLista = idsTareas.ToList();
                var idsAcuerdosLista = idsAcuerdos.ToList();
                var idsTemasLista = idsTemas.ToList();
                if (idsTareasLista.Count == 0)
                    await _db.Deleteable<MinutaTareaEntity>().Where(x => x.MinutaId == detalle.MinutaId).ExecuteCommandAsync().ConfigureAwait(false);
                else
                    await _db.Deleteable<MinutaTareaEntity>().Where(x => x.MinutaId == detalle.MinutaId && !idsTareasLista.Contains(x.Id)).ExecuteCommandAsync().ConfigureAwait(false);
                if (idsAcuerdosLista.Count == 0)
                    await _db.Deleteable<MinutaAcuerdoEntity>().Where(x => x.MinutaId == detalle.MinutaId).ExecuteCommandAsync().ConfigureAwait(false);
                else
                    await _db.Deleteable<MinutaAcuerdoEntity>().Where(x => x.MinutaId == detalle.MinutaId && !idsAcuerdosLista.Contains(x.Id)).ExecuteCommandAsync().ConfigureAwait(false);
                if (idsTemasLista.Count == 0)
                    await _db.Deleteable<MinutaTemaEntity>().Where(x => x.MinutaId == detalle.MinutaId).ExecuteCommandAsync().ConfigureAwait(false);
                else
                    await _db.Deleteable<MinutaTemaEntity>().Where(x => x.MinutaId == detalle.MinutaId && !idsTemasLista.Contains(x.Id)).ExecuteCommandAsync().ConfigureAwait(false);
                var idsAsistentes = new HashSet<long>(detalle.Asistentes.Where(x => x.Id > 0).Select(x => x.Id));
                var idsAsistentesLista = idsAsistentes.ToList();
                if (idsAsistentesLista.Count == 0)
                    await _db.Deleteable<MinutaAsistenteEntity>().Where(x => x.MinutaId == detalle.MinutaId).ExecuteCommandAsync().ConfigureAwait(false);
                else
                    await _db.Deleteable<MinutaAsistenteEntity>().Where(x => x.MinutaId == detalle.MinutaId && !idsAsistentesLista.Contains(x.Id)).ExecuteCommandAsync().ConfigureAwait(false);

                var temaIdsPorOrden = new Dictionary<int, long>();
                for (int i = 0; i < detalle.Temas.Count; i++) {
                    var d = detalle.Temas[i];
                    var e = new MinutaTemaEntity { Id = d.Id, MinutaId = detalle.MinutaId, Orden = i + 1, Tema = d.Tema, Descripcion = d.Descripcion, ResponsableId = d.ResponsableId, Estado = d.Estado };
                    if (d.Id > 0)
                        await _db.Updateable(e).Where(x => x.Id == d.Id && x.MinutaId == detalle.MinutaId).ExecuteCommandAsync().ConfigureAwait(false);
                    else
                        d.Id = await _db.Insertable(e).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                    d.Orden = i + 1;
                    temaIdsPorOrden[d.Orden] = d.Id;
                }

                var acuerdoIdsPorOrden = new Dictionary<int, long>();
                for (int i = 0; i < detalle.Acuerdos.Count; i++) {
                    var d = detalle.Acuerdos[i];
                    long? temaId = null;
                    if (d.TemaOrdenSeleccionado.HasValue && temaIdsPorOrden.ContainsKey(d.TemaOrdenSeleccionado.Value))
                        temaId = temaIdsPorOrden[d.TemaOrdenSeleccionado.Value];
                    else if (d.TemaId.HasValue && detalle.Temas.Any(x => x.Id == d.TemaId.Value))
                        temaId = d.TemaId;
                    var e = new MinutaAcuerdoEntity { Id = d.Id, MinutaId = detalle.MinutaId, TemaId = temaId, Tipo = d.Tipo, Descripcion = d.Descripcion, Orden = i + 1, RegistradoPor = d.Id > 0 ? d.RegistradoPor : usuarioId, FechaRegistro = d.FechaRegistro };
                    if (d.Id > 0)
                        await _db.Updateable(e).Where(x => x.Id == d.Id && x.MinutaId == detalle.MinutaId).ExecuteCommandAsync().ConfigureAwait(false);
                    else
                        d.Id = await _db.Insertable(e).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                    d.Orden = i + 1;
                    d.TemaId = temaId;
                    d.RegistradoPor = e.RegistradoPor;
                    acuerdoIdsPorOrden[d.Orden] = d.Id;
                }

                for (int i = 0; i < detalle.Tareas.Count; i++) {
                    var d = detalle.Tareas[i];
                    long? acuerdoId = null;
                    if (d.AcuerdoOrdenSeleccionado.HasValue && acuerdoIdsPorOrden.ContainsKey(d.AcuerdoOrdenSeleccionado.Value))
                        acuerdoId = acuerdoIdsPorOrden[d.AcuerdoOrdenSeleccionado.Value];
                    else if (d.AcuerdoId.HasValue && detalle.Acuerdos.Any(x => x.Id == d.AcuerdoId.Value))
                        acuerdoId = d.AcuerdoId;
                    var e = new MinutaTareaEntity { Id = d.Id, MinutaId = detalle.MinutaId, AcuerdoId = acuerdoId, Descripcion = d.Descripcion, ResponsableId = d.ResponsableId, FechaLimite = d.FechaLimite.Date, Estado = d.Estado, FechaFinalizacion = d.FechaFinalizacion, Evidencia = d.Evidencia, CreadoPor = d.Id > 0 ? d.CreadoPor : usuarioId };
                    if (d.Id > 0)
                        await _db.Updateable(e).Where(x => x.Id == d.Id && x.MinutaId == detalle.MinutaId).ExecuteCommandAsync().ConfigureAwait(false);
                    else
                        d.Id = await _db.Insertable(e).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                    d.AcuerdoId = acuerdoId;
                    d.CreadoPor = e.CreadoPor;
                }

                foreach (var d in detalle.Asistentes) {
                    var e = new MinutaAsistenteEntity { Id = d.Id, MinutaId = detalle.MinutaId, UsuarioId = d.IdUsuario, Nombre = d.Nombre, Correo = d.Correo, Rol = d.Rol, EstadoAsistencia = d.EstadoAsistencia };
                    if (d.Id > 0)
                        await _db.Updateable(e).Where(x => x.Id == d.Id && x.MinutaId == detalle.MinutaId).ExecuteCommandAsync().ConfigureAwait(false);
                    else
                        d.Id = await _db.Insertable(e).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                }
                var serializadorAuditoria = new System.Web.Script.Serialization.JavaScriptSerializer { MaxJsonLength = int.MaxValue };
                string resumenNuevo = serializadorAuditoria.Serialize(new {
                    Asistentes = detalle.Asistentes.Select(x => new { x.Id, x.IdUsuario, x.Nombre, x.Correo, x.Rol, x.EstadoAsistencia }).ToArray(),
                    Temas = detalle.Temas.Select(x => new { x.Id, x.Orden, x.Tema, x.Descripcion, x.ResponsableId, x.Estado }).ToArray(),
                    Acuerdos = detalle.Acuerdos.Select(x => new { x.Id, x.TemaId, x.Tipo, x.Descripcion, x.Orden, x.RegistradoPor }).ToArray(),
                    Tareas = detalle.Tareas.Select(x => new { x.Id, x.AcuerdoId, x.Descripcion, x.ResponsableId, x.FechaLimite, x.Estado, x.FechaFinalizacion, x.Evidencia, x.CreadoPor }).ToArray()
                });
                string resumenAnterior = serializadorAuditoria.Serialize(new {
                    Asistentes = asistentesActuales.Select(x => new { x.Id, x.UsuarioId, x.Nombre, x.Correo, x.Rol, x.EstadoAsistencia }).ToArray(),
                    Temas = temasActuales.Select(x => new { x.Id, x.Orden, x.Tema, x.Descripcion, x.ResponsableId, x.Estado }).ToArray(),
                    Acuerdos = acuerdosActuales.Select(x => new { x.Id, x.TemaId, x.Tipo, x.Descripcion, x.Orden, x.RegistradoPor }).ToArray(),
                    Tareas = tareasActuales.Select(x => new { x.Id, x.AcuerdoId, x.Descripcion, x.ResponsableId, x.FechaLimite, x.Estado, x.FechaFinalizacion, x.Evidencia, x.CreadoPor }).ToArray()
                });
                await MinutaAuditoriaWriter.RegistrarAsync(_db, detalle.MinutaId, usuarioId, "ACTUALIZAR_DETALLE", resumenAnterior, resumenNuevo).ConfigureAwait(false);
                _db.Ado.CommitTran();
                detalle.VersionFila++;
            } catch (MinutaConcurrenteException ex) {
                _db.Ado.RollbackTran();
                throw new InvalidOperationException("La minuta fue modificada por otro usuario desde que se abrió. Sus cambios no se guardaron. Recargue la minuta y vuelva a aplicar sus modificaciones.", ex);
            } catch {
                _db.Ado.RollbackTran();
                throw;
            }
        }
    }
}
