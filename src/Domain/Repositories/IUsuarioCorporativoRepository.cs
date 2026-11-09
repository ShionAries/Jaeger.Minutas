using System.Collections.Generic;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.Domain.Repositories {
    public interface IUsuarioCorporativoRepository {
        Task<List<UsuarioCorporativo>> ObtenerActivosAsync();
    }
}
