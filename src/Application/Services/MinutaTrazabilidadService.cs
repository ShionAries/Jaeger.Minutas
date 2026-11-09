using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Repositories;
namespace Jaeger.Minutas.Aplication.Services {
    public sealed class MinutaTrazabilidadService {
        private readonly IMinutaTrazabilidadRepository _repository;

        public MinutaTrazabilidadService(IMinutaTrazabilidadRepository repository) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<MinutaSeguimientoPendiente>> ObtenerPendientesAsync(long minutaOrigenId) {
            if (minutaOrigenId <= 0) throw new ArgumentOutOfRangeException(nameof(minutaOrigenId));
            return _repository.ObtenerPendientesAsync(minutaOrigenId);
        }

        public async Task<long> CrearSeguimientoAsync(long minutaOrigenId, Minuta minuta, IEnumerable<long> tareasIds, long usuarioId) {
            if (minuta == null) throw new ArgumentNullException(nameof(minuta));
            if (usuarioId <= 0) throw new ArgumentOutOfRangeException(nameof(usuarioId));
            if (string.IsNullOrWhiteSpace(minuta.Titulo) || string.IsNullOrWhiteSpace(minuta.Objetivo))
                throw new ArgumentException("El título y el objetivo de la minuta son obligatorios.");
            var ids = (tareasIds ?? Enumerable.Empty<long>()).Distinct().ToList();
            if (ids.Any(x => x <= 0)) throw new ArgumentException("La selección contiene una tarea inválida.");
            minuta.RegistrarCreacion(usuarioId);
            return await _repository.CrearSeguimientoAsync(minutaOrigenId, minuta, ids, usuarioId).ConfigureAwait(false);
        }

        public Task<List<MinutaTrazabilidadTarea>> ObtenerTareasRelacionadasAsync(long minutaId) {
            if (minutaId <= 0) throw new ArgumentOutOfRangeException(nameof(minutaId));
            return _repository.ObtenerTareasRelacionadasAsync(minutaId);
        }

        public Task<List<MinutaTrazabilidadNodo>> ObtenerCadenaAsync(long minutaId) {
            if (minutaId <= 0) throw new ArgumentOutOfRangeException(nameof(minutaId));
            return _repository.ObtenerCadenaAsync(minutaId);
        }
    }
}
