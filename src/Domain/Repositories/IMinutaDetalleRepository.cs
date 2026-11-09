using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.Domain.Repositories {
    public interface IMinutaDetalleRepository {
        Task<MinutaDetalle> ObtenerAsync(long minutaId);
        Task GuardarAsync(MinutaDetalle detalle, long usuarioId);
    }
}
