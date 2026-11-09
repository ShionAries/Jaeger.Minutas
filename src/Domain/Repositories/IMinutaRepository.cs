using System.Collections.Generic;
using System.Threading.Tasks;
using Jaeger.Minutas.DataAccess.Entities;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;

namespace Jaeger.Minutas.Domain.Repositories {
    public interface IMinutaRepository {
        Task<long> CrearAsync(MinutaDetail minuta, long usuarioId);
        Task<Minuta> ObtenerPorIdAsync(long id);
        Task<List<MinutaEntity>> ObtenerTodosAsync();
        Task CambiarEstadoAsync(long id, EstadoMinuta estadoActual, EstadoMinuta nuevoEstado, int versionEsperada);
        Task EliminarAsync(long id, int versionEsperada);
    }
}
