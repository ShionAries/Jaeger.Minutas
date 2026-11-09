using System.Collections.Generic;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.Application.Contracts {
    public interface IMinutaService {
        Task<List<Minuta>> ObtenerMinutasAsync();
        Task<MinutaDetail> GuardarAsync(MinutaDetail minuta);
        Task EliminarAsync(int id);
    }
}
