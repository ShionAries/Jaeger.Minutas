using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jaeger.Minutas.DataAccess.Mapping;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.Aplication.Services {
    public sealed class CrearMinutaService {
        private readonly IMinutaRepository _repository;

        public CrearMinutaService(IMinutaRepository repository) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Minuta>> ObtenerMinutasAsync() {
            var entities = await _repository.ObtenerTodosAsync().ConfigureAwait(false);
            return entities.Select(MinutaMapper.ToDomain).ToList();
        }
    }
}
