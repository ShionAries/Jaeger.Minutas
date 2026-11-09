using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using Jaeger.Minutas.DataAccess.Entities;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.DataAccess.Repositories {
    public sealed class MinutaAuditoriaRepository : IMinutaAuditoriaRepository {
        private readonly SqlSugarClient _db;

        public MinutaAuditoriaRepository(SqlSugarClient db) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<List<MinutaAuditoriaRegistro>> BuscarAsync(MinutaAuditoriaFiltro filtro) {
            if (filtro == null)
                throw new ArgumentNullException(nameof(filtro));

            long? minutaId = null;
            if (!string.IsNullOrWhiteSpace(filtro.FolioOIdMinuta)) {
                long id;
                if (long.TryParse(filtro.FolioOIdMinuta, out id)) {
                    minutaId = id;
                } else {
                    MinutaEntity minuta = await _db.Queryable<MinutaEntity>()
                        .Where(x => x.Folio == filtro.FolioOIdMinuta)
                        .FirstAsync().ConfigureAwait(false);
                    if (minuta == null)
                        return new List<MinutaAuditoriaRegistro>();
                    minutaId = minuta.Id;
                }
            }

            DateTime? desde = filtro.Desde.HasValue ? (DateTime?)filtro.Desde.Value.Date : null;
            DateTime? hastaExclusiva = filtro.Hasta.HasValue ? (DateTime?)filtro.Hasta.Value.Date.AddDays(1) : null;
            List<MinutaAuditoriaEntity> eventos = await _db.Queryable<MinutaAuditoriaEntity>()
                .WhereIF(minutaId.HasValue, x => x.MinutaId == minutaId.Value)
                .WhereIF(filtro.UsuarioId.HasValue, x => x.UsuarioId == filtro.UsuarioId.Value)
                .WhereIF(!string.IsNullOrWhiteSpace(filtro.Accion), x => x.Accion == filtro.Accion)
                .WhereIF(desde.HasValue, x => x.FechaEvento >= desde.Value)
                .WhereIF(hastaExclusiva.HasValue, x => x.FechaEvento < hastaExclusiva.Value)
                .OrderBy(x => x.FechaEvento, OrderByType.Desc)
                .OrderBy(x => x.Id, OrderByType.Desc)
                .Take(filtro.Limite)
                .ToListAsync().ConfigureAwait(false);

            if (eventos.Count == 0)
                return new List<MinutaAuditoriaRegistro>();

            List<int> usuarioIds = eventos.Select(x => x.UsuarioId).Distinct().ToList();
            List<long> minutaIds = eventos.Select(x => x.MinutaId).Distinct().ToList();
            List<UsuarioCorporativoEntity> usuarios = await _db.Queryable<UsuarioCorporativoEntity>()
                .Where(x => usuarioIds.Contains(x.Id)).ToListAsync().ConfigureAwait(false);
            List<MinutaEntity> minutas = await _db.Queryable<MinutaEntity>()
                .Where(x => minutaIds.Contains(x.Id)).ToListAsync().ConfigureAwait(false);

            Dictionary<int, string> nombres = usuarios.ToDictionary(x => x.Id, x => string.IsNullOrWhiteSpace(x.Nombre) ? "Usuario " + x.Id : x.Nombre.Trim());
            Dictionary<long, MinutaEntity> datosMinutas = minutas.ToDictionary(x => x.Id);

            return eventos.Select(x => {
                MinutaEntity minuta;
                string usuario;
                datosMinutas.TryGetValue(x.MinutaId, out minuta);
                nombres.TryGetValue(x.UsuarioId, out usuario);
                return new MinutaAuditoriaRegistro {
                    Id = x.Id,
                    MinutaId = x.MinutaId,
                    Folio = minuta == null ? string.Empty : minuta.Folio,
                    TituloMinuta = minuta == null ? string.Empty : minuta.Titulo,
                    UsuarioId = x.UsuarioId,
                    Usuario = usuario ?? "Usuario " + x.UsuarioId,
                    Accion = x.Accion,
                    FechaEvento = x.FechaEvento,
                    DatosAnteriores = x.DatosAnteriores,
                    DatosNuevos = x.DatosNuevos
                };
            }).ToList();
        }
    }
}
