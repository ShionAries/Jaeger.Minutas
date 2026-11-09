using System.Collections.Generic;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;
namespace Jaeger.Minutas.Domain.Repositories {
    public interface IMinutaTrazabilidadRepository {
        Task<List<MinutaSeguimientoPendiente>> ObtenerPendientesAsync(long minutaOrigenId);
        Task<long> CrearSeguimientoAsync(long minutaOrigenId, Minuta nuevaMinuta, IList<long> tareasOrigenIds, long usuarioId);
        Task<List<MinutaTrazabilidadNodo>> ObtenerCadenaAsync(long minutaId);
        Task<List<MinutaTrazabilidadTarea>> ObtenerTareasRelacionadasAsync(long minutaId);
    }
}
