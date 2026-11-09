using System;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.Aplication.Services {
    public sealed class ObtenerMinutaService {
        private readonly IMinutaRepository _repository;

        public ObtenerMinutaService(IMinutaRepository repository) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Minuta> EjecutarAsync(long id) {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _repository.ObtenerPorIdAsync(id).ConfigureAwait(false);
        }
    }
}
