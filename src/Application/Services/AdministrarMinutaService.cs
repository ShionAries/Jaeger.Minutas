using System;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;
using Jaeger.Minutas.Domain.Exceptions;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.Aplication.Services {
    public sealed class AdministrarMinutaService {
        private readonly IMinutaRepository _repository;
        public AdministrarMinutaService(IMinutaRepository repository) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        public async Task CambiarEstadoAsync(long id, int versionEsperada, EstadoMinuta nuevoEstado) {
            if (versionEsperada <= 0)
                throw new ArgumentOutOfRangeException(nameof(versionEsperada));
            Minuta minuta = await _repository.ObtenerPorIdAsync(id).ConfigureAwait(false);
            if (minuta == null)
                throw new InvalidOperationException("La minuta no existe o está eliminada.");
            if (minuta.VersionFila != versionEsperada)
                throw new MinutaConcurrenteException();
            EstadoMinuta anterior = minuta.Estado;
            minuta.CambiarEstado(nuevoEstado);
            await _repository.CambiarEstadoAsync(id, anterior, nuevoEstado, versionEsperada).ConfigureAwait(false);
        }

        public async Task EliminarAsync(long id) {
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
