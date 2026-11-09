using System;
using System.Linq;
using System.Threading.Tasks;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Repositories;

namespace Jaeger.Minutas.Aplication.Services {
    public sealed class MinutaDetalleService {
        private readonly IMinutaDetalleRepository _repository;
        public MinutaDetalleService(IMinutaDetalleRepository repository) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<MinutaDetalle> ObtenerAsync(long minutaId) {
            if (minutaId <= 0) throw new ArgumentOutOfRangeException(nameof(minutaId));
            return await _repository.ObtenerAsync(minutaId).ConfigureAwait(false);
        }

        public async Task GuardarAsync(MinutaDetalle detalle, long usuarioId) {
            if (detalle == null) throw new ArgumentNullException(nameof(detalle));
            if (detalle.MinutaId <= 0) throw new ArgumentOutOfRangeException(nameof(detalle.MinutaId));
            if (usuarioId <= 0) throw new ArgumentOutOfRangeException(nameof(usuarioId));
            if (detalle.Asistentes == null || detalle.Temas == null || detalle.Acuerdos == null || detalle.Tareas == null)
                throw new ArgumentException("Las colecciones de detalle deben inicializarse.", nameof(detalle));

            var asistentesDuplicados = detalle.Asistentes.Where(x => x.IdUsuario.HasValue)
                .GroupBy(x => x.IdUsuario.Value).Any(x => x.Count() > 1);
            if (asistentesDuplicados)
                throw new ArgumentException("No se puede agregar dos veces al mismo usuario como asistente.");

            foreach (var a in detalle.Asistentes) {
                if (string.IsNullOrWhiteSpace(a.Nombre)) throw new ArgumentException("Cada asistente debe tener nombre.");
                if (string.IsNullOrWhiteSpace(a.Rol)) throw new ArgumentException("Cada asistente debe tener rol.");
                if (string.IsNullOrWhiteSpace(a.EstadoAsistencia)) a.EstadoAsistencia = "Convocado";
                a.Nombre = a.Nombre.Trim(); a.Rol = a.Rol.Trim();
            }
            for (int i = 0; i < detalle.Temas.Count; i++) {
                var t = detalle.Temas[i];
                if (string.IsNullOrWhiteSpace(t.Tema)) throw new ArgumentException("Cada tema debe tener descripción.");
                t.Tema = t.Tema.Trim(); t.Orden = i + 1;
                if (string.IsNullOrWhiteSpace(t.Estado)) t.Estado = "Pendiente";
            }
            for (int i = 0; i < detalle.Acuerdos.Count; i++) {
                var a = detalle.Acuerdos[i];
                if (string.IsNullOrWhiteSpace(a.Descripcion)) throw new ArgumentException("Cada acuerdo debe tener descripción.");
                if (string.IsNullOrWhiteSpace(a.Tipo)) throw new ArgumentException("Cada acuerdo debe tener tipo.");
                a.Descripcion = a.Descripcion.Trim(); a.Orden = i + 1; if (a.RegistradoPor <= 0) a.RegistradoPor = usuarioId;
            }
            foreach (var a in detalle.Acuerdos) {
                if (a.TemaOrdenSeleccionado.HasValue &&
                    (a.TemaOrdenSeleccionado.Value < 1 || a.TemaOrdenSeleccionado.Value > detalle.Temas.Count))
                    throw new ArgumentException("Un acuerdo está relacionado con un tema que ya no existe.");
            }
            foreach (var t in detalle.Tareas) {
                if (t.AcuerdoOrdenSeleccionado.HasValue &&
                    (t.AcuerdoOrdenSeleccionado.Value < 1 || t.AcuerdoOrdenSeleccionado.Value > detalle.Acuerdos.Count))
                    throw new ArgumentException("Una tarea está relacionada con un acuerdo que ya no existe.");
                if (string.IsNullOrWhiteSpace(t.Descripcion)) throw new ArgumentException("Cada tarea debe tener descripción.");
                if (t.ResponsableId <= 0) throw new ArgumentException("Cada tarea debe tener un responsable válido.");
                if (t.FechaLimite == DateTime.MinValue) throw new ArgumentException("Cada tarea debe tener fecha límite.");
                if (string.IsNullOrWhiteSpace(t.Estado)) t.Estado = "Pendiente";
                t.Descripcion = t.Descripcion.Trim(); if (t.CreadoPor <= 0) t.CreadoPor = usuarioId;
            }

            await _repository.GuardarAsync(detalle, usuarioId).ConfigureAwait(false);
        }
    }
}
