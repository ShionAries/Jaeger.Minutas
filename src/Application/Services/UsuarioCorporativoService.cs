using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.Aplication.Services {
    public sealed class UsuarioCorporativoService {
        private readonly IUsuarioCorporativoRepository _repository;

        public UsuarioCorporativoService(IUsuarioCorporativoRepository repository) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<UsuarioCorporativo>> ObtenerActivosAsync() {
            return _repository.ObtenerActivosAsync();
        }
    }
}
