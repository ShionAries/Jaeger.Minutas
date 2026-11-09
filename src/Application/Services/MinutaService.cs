using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jaeger.Minutas.Application.Contracts;
using Jaeger.Minutas.Application.Services;
using Jaeger.Minutas.DataAccess.Mapping;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.Aplication.Services {
    public class MinutaService : IMinutaService {
        private readonly IMinutaRepository _repository;
        public MinutaService(IMinutaRepository repository) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Minuta>> ObtenerMinutasAsync() {
            var entities = await _repository.ObtenerTodosAsync().ConfigureAwait(false);
            return entities.Select(MinutaMapper.ToDomain).ToList();
        }

        public async Task<MinutaDetail> GuardarAsync(MinutaDetail minuta) {
            if (minuta == null)
                throw new ArgumentNullException(nameof(minuta));
            if (ConfigService.IdUsuario <= 0)
                throw new ArgumentOutOfRangeException(nameof(ConfigService.IdUsuario));

            minuta.RegistrarCreacion(ConfigService.IdUsuario);
            long id = await _repository.CrearAsync(minuta, ConfigService.IdUsuario).ConfigureAwait(false);
            minuta.AsignarId(id);
            minuta.AsignarFolio("MIN-" + id.ToString("D6"));
            return minuta;
        }
        
        public async Task EliminarAsync(int id) {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            Minuta minuta = await _repository.ObtenerPorIdAsync(id).ConfigureAwait(false);
            if (minuta == null)
                throw new InvalidOperationException("La minuta no existe o ya fue eliminada.");
            if (minuta.Estado == EstadoMinuta.Aprobada || minuta.Estado == EstadoMinuta.Cancelada)
                throw new InvalidOperationException("No se puede eliminar una minuta aprobada o cancelada.");
            await _repository.EliminarAsync(id, minuta.VersionFila).ConfigureAwait(false);
        }
    }
}
