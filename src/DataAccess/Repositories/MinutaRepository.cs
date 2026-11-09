using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using Jaeger.Minutas.DataAccess.Entities;
using Jaeger.Minutas.DataAccess.Mapping;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;
using Jaeger.Minutas.Domain.Exceptions;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.DataAccess.Repositories {
    public sealed class MinutaRepository : IMinutaRepository {
        private readonly SqlSugarClient _db;

        public MinutaRepository(SqlSugarClient db) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<long> CrearAsync(MinutaDetail minuta, long usuarioId) {
            if (minuta == null)
                throw new ArgumentNullException(nameof(minuta));
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));
            if (minuta.Asistentes == null)
                minuta.Asistentes = new List<MinutaAsistente>();

            var asistentes = minuta.Asistentes;
            if (asistentes.Any(x => x == null))
                throw new InvalidOperationException("La lista de asistentes contiene un registro inválido.");

            foreach (long idUsuario in asistentes.Where(x => x.IdUsuario.HasValue).Select(x => x.IdUsuario.Value).Distinct()) {
                if (!await _db.Queryable<UsuarioCorporativoEntity>()
                    .AnyAsync(x => x.Id == idUsuario && x.Activo == 1).ConfigureAwait(false)) {
                    throw new InvalidOperationException("Uno de los asistentes seleccionados no existe o está inactivo en el catálogo corporativo.");
                }
            }

            if (asistentes.Any(x => !x.IdUsuario.HasValue || x.IdUsuario.Value <= 0))
                throw new InvalidOperationException("Todos los asistentes deben estar asociados a un usuario corporativo válido.");

            var idsUsuarios = asistentes.Select(x => x.IdUsuario.Value).ToList();
            if (idsUsuarios.Distinct().Count() != idsUsuarios.Count)
                throw new InvalidOperationException("No puede registrar dos veces al mismo usuario como asistente.");

            MinutaEntity entity = MinutaMapper.ToEntity(minuta, usuarioId);
            entity.Folio = "TMP-" + Guid.NewGuid().ToString("N").Substring(0, 24);

            _db.Ado.BeginTran();
            try {
                long id = await _db.Insertable(entity).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                string folioGenerado = "MIN-" + id.ToString("D6");
                int filasFolio = await _db.Ado.ExecuteCommandAsync(
                    "UPDATE MNTA SET MNTA_FOLIO=@folio WHERE MNTA_ID=@id",
                    new SugarParameter("@folio", folioGenerado),
                    new SugarParameter("@id", id)).ConfigureAwait(false);

                if (filasFolio != 1)
                    throw new InvalidOperationException("No fue posible asignar el folio automático de la minuta.");

                entity.Id = id;
                entity.Folio = folioGenerado;

                foreach (MinutaAsistente asistente in asistentes) {
                    var registro = new MinutaAsistenteEntity {
                        MinutaId = id,
                        UsuarioId = asistente.IdUsuario,
                        Nombre = asistente.Nombre,
                        Correo = asistente.Correo,
                        Rol = string.IsNullOrWhiteSpace(asistente.Rol) ? "Asistente" : asistente.Rol.Trim(),
                        EstadoAsistencia = string.IsNullOrWhiteSpace(asistente.EstadoAsistencia)
                            ? "Convocado"
                            : asistente.EstadoAsistencia.Trim()
                    };

                    asistente.Id = await _db.Insertable(registro).ExecuteReturnBigIdentityAsync().ConfigureAwait(false);
                    asistente.IdMinuta = id;
                }

                await MinutaAuditoriaWriter.RegistrarAsync(
                    _db, id, usuarioId, "CREAR", null, MinutaAuditoriaWriter.Snapshot(entity)).ConfigureAwait(false);

                _db.Ado.CommitTran();
                return id;
            } catch {
                _db.Ado.RollbackTran();
                throw;
            }
        }

        public async Task<Minuta> ObtenerPorIdAsync(long id) {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            MinutaEntity entity = await _db.Queryable<MinutaEntity>()
                .Where(x => x.Id == id && !x.Eliminado)
                .FirstAsync()
                .ConfigureAwait(false);

            return MinutaMapper.ToDomain(entity);
        }

        public Task<List<MinutaEntity>> ObtenerTodosAsync() {
            return _db.Queryable<MinutaEntity>()
                .Where(x => !x.Eliminado)
                .ToListAsync();
        }

        public async Task CambiarEstadoAsync(long id, EstadoMinuta estadoActual, EstadoMinuta nuevoEstado, int versionEsperada) {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (versionEsperada <= 0)
                throw new ArgumentOutOfRangeException(nameof(versionEsperada));
            int filas = await _db.Ado.ExecuteCommandAsync(
                "UPDATE MNTA SET MNTA_STTS_ID=@nuevoEstado, MNTA_FCACT=CURRENT_TIMESTAMP, MNTA_VERFIL=MNTA_VERFIL+1 WHERE MNTA_ID=@id AND MNTA_STTS_ID=@estadoActual AND MNTA_VERFIL=@version AND MNTA_A=0",
                new SugarParameter("@nuevoEstado", nuevoEstado.ToString()), new SugarParameter("@id", id),
                new SugarParameter("@estadoActual", estadoActual.ToString()), new SugarParameter("@version", versionEsperada)).ConfigureAwait(false);
            if (filas != 1)
                throw new MinutaConcurrenteException();
        }

        public async Task EliminarAsync(long id, int versionEsperada) {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (versionEsperada <= 0)
                throw new ArgumentOutOfRangeException(nameof(versionEsperada));
            int filas = await _db.Ado.ExecuteCommandAsync(
                "UPDATE MNTA SET MNTA_A=1, MNTA_FCACT=CURRENT_TIMESTAMP, MNTA_VERFIL=MNTA_VERFIL+1 WHERE MNTA_ID=@id AND MNTA_VERFIL=@version AND MNTA_A=0 AND MNTA_STTS_ID NOT IN ('Aprobada','Cancelada')",
                new SugarParameter("@id", id), new SugarParameter("@version", versionEsperada)).ConfigureAwait(false);
            if (filas != 1)
                throw new MinutaConcurrenteException();
        }
    }
}
