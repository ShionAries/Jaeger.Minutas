using System.Collections.Generic;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.Domain.Repositories {
    public interface IMinutaAuditoriaRepository {
        Task<List<MinutaAuditoriaRegistro>> BuscarAsync(MinutaAuditoriaFiltro filtro);
    }
}
