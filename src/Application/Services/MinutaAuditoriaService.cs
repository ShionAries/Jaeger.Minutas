using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.Aplication.Services {
    public sealed class MinutaAuditoriaService {
        private readonly IMinutaAuditoriaRepository _repository;

        public MinutaAuditoriaService(IMinutaAuditoriaRepository repository) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<MinutaAuditoriaRegistro>> BuscarAsync(MinutaAuditoriaFiltro filtro) {
            if (filtro == null)
                throw new ArgumentNullException(nameof(filtro));
            if (filtro.UsuarioId.HasValue && filtro.UsuarioId.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(filtro.UsuarioId));
            if (filtro.Desde.HasValue && filtro.Hasta.HasValue && filtro.Desde.Value.Date > filtro.Hasta.Value.Date)
                throw new ArgumentException("La fecha inicial no puede ser posterior a la fecha final.");
            if (filtro.Limite < 1 || filtro.Limite > 5000)
                filtro.Limite = 500;
            filtro.FolioOIdMinuta = string.IsNullOrWhiteSpace(filtro.FolioOIdMinuta) ? null : filtro.FolioOIdMinuta.Trim();
            filtro.Accion = string.IsNullOrWhiteSpace(filtro.Accion) ? null : filtro.Accion.Trim();
            return _repository.BuscarAsync(filtro);
        }
    }
}
