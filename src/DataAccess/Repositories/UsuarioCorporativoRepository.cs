using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jaeger.Minutas.DataAccess.Entities;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Repositories;
using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Repositories {
    public sealed class UsuarioCorporativoRepository : IUsuarioCorporativoRepository {
        private readonly SqlSugarClient _db;

        public UsuarioCorporativoRepository(SqlSugarClient db) {
            _db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        public async Task<List<UsuarioCorporativo>> ObtenerActivosAsync() {
            List<UsuarioCorporativoEntity> registros = await _db.Queryable<UsuarioCorporativoEntity>()
                .Where(x => x.Activo == 1)
                .OrderBy(x => x.Nombre)
                .ToListAsync().ConfigureAwait(false);

            return registros.Select(x => new UsuarioCorporativo {
                Id = x.Id, Nombre = x.Nombre, Correo = x.Correo
            }).ToList();
        }
    }
}
