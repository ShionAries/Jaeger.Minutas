using System;
using Jaeger.Minutas.Domain.Enums;

namespace Jaeger.Minutas.Domain.Entities {
    public class Minuta {
        #region propiedades
        public long IdMinuta { get; private set; }
        public string Folio { get; private set; }
        public string Titulo { get; private set; }
        public string Tipo { get; private set; }
        public DateTime FechaReunion { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan? HoraFin { get; private set; }
        public string Lugar { get; private set; }
        public string Plataforma { get; private set; }
        public string Objetivo { get; private set; }
        public long? DepartamentoId { get; private set; }
        public long? ProyectoId { get; private set; }
        public long? ClienteId { get; private set; }
        public EstadoMinuta Estado { get; private set; }
        public DateTime? ProximaFecha { get; private set; }
        public string ProximaLugar { get; private set; }
        public long Creo { get; private set; }
        public int VersionFila { get; private set; }
        #endregion

        public Minuta(string folio, string titulo, DateTime fechaReunion, TimeSpan horaInicio, string objetivo) {
            if (string.IsNullOrWhiteSpace(folio))
                throw new ArgumentException("El folio es obligatorio.", nameof(folio));
            if (folio.Trim().Length > 30)
                throw new ArgumentException("El folio no puede exceder 30 caracteres.", nameof(folio));
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título es obligatorio.", nameof(titulo));
            if (titulo.Trim().Length > 200)
                throw new ArgumentException("El título no puede exceder 200 caracteres.", nameof(titulo));
            if (string.IsNullOrWhiteSpace(objetivo))
                throw new ArgumentException("El objetivo es obligatorio.", nameof(objetivo));
            if (horaInicio < TimeSpan.Zero || horaInicio >= TimeSpan.FromDays(1))
                throw new ArgumentOutOfRangeException(nameof(horaInicio));

            Folio = folio.Trim();
            Titulo = titulo.Trim();
            FechaReunion = fechaReunion.Date;
            HoraInicio = horaInicio;
            Objetivo = objetivo.Trim();
            Tipo = "General";
            Estado = EstadoMinuta.Borrador;
            VersionFila = 1;
        }

        public Minuta(string titulo, DateTime fechaReunion, TimeSpan horaInicio, string objetivo) {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título es obligatorio.", nameof(titulo));
            if (titulo.Trim().Length > 200)
                throw new ArgumentException("El título no puede exceder 200 caracteres.", nameof(titulo));
            if (string.IsNullOrWhiteSpace(objetivo))
                throw new ArgumentException("El objetivo es obligatorio.", nameof(objetivo));
            if (horaInicio < TimeSpan.Zero || horaInicio >= TimeSpan.FromDays(1))
                throw new ArgumentOutOfRangeException(nameof(horaInicio));

            Titulo = titulo.Trim();
            FechaReunion = fechaReunion.Date;
            HoraInicio = horaInicio;
            Objetivo = objetivo.Trim();
            Tipo = "General";
            Estado = EstadoMinuta.Borrador;
            VersionFila = 1;
        }

        #region metodos
        public void AsignarId(long id) {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (IdMinuta != 0)
                throw new InvalidOperationException("La minuta ya tiene identificador.");
            IdMinuta = id;
        }

        public void AsignarFolio(string folio) {
            if (string.IsNullOrWhiteSpace(folio))
                throw new ArgumentException("El folio generado es obligatorio.", nameof(folio));
            if (folio.Trim().Length > 30)
                throw new ArgumentException("El folio no puede exceder 30 caracteres.", nameof(folio));
            if (Folio != null && !string.Equals(Folio, folio.Trim(), StringComparison.Ordinal))
                throw new InvalidOperationException("El folio de la minuta no se puede modificar.");
            Folio = folio.Trim();
        }

        public void RegistrarCreacion(long usuarioId) {
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));
            Creo = usuarioId;
        }

        public void DefinirClasificacion(string tipo, long? departamentoId, long? proyectoId, long? clienteId) {
            if (string.IsNullOrWhiteSpace(tipo) || tipo.Trim().Length > 30)
                throw new ArgumentException("El tipo es obligatorio y admite hasta 30 caracteres.", nameof(tipo));
            Tipo = tipo.Trim();
            DepartamentoId = departamentoId;
            ProyectoId = proyectoId;
            ClienteId = clienteId;
        }

        public void DefinirUbicacion(string lugar, string plataforma) {
            if (lugar != null && lugar.Trim().Length > 250)
                throw new ArgumentException("El lugar no puede exceder 250 caracteres.", nameof(lugar));
            if (plataforma != null && plataforma.Trim().Length > 100)
                throw new ArgumentException("La plataforma no puede exceder 100 caracteres.", nameof(plataforma));
            Lugar = NormalizarOpcional(lugar);
            Plataforma = NormalizarOpcional(plataforma);
        }

        public void DefinirFin(TimeSpan horaFin) {
            if (horaFin <= HoraInicio || horaFin >= TimeSpan.FromDays(1))
                throw new ArgumentException("La hora de fin debe ser posterior al inicio y dentro del día.", nameof(horaFin));
            HoraFin = horaFin;
        }

        public void DefinirProximaReunion(DateTime? fecha, string lugar) {
            if (lugar != null && lugar.Trim().Length > 250)
                throw new ArgumentException("El lugar de la próxima reunión no puede exceder 250 caracteres.", nameof(lugar));
            if (fecha.HasValue && fecha.Value < FechaReunion)
                throw new ArgumentException("La próxima reunión no puede ser anterior a la reunión actual.", nameof(fecha));
            ProximaFecha = fecha;
            ProximaLugar = NormalizarOpcional(lugar);
        }

        public void ActualizarDatosGenerales(string titulo, string tipo, DateTime fechaReunion, TimeSpan horaInicio, TimeSpan? horaFin, string lugar, string plataforma, string objetivo) {
            AsegurarEditable();
            var validacion = new Minuta(titulo, fechaReunion, horaInicio, objetivo);
            validacion.DefinirClasificacion(tipo, DepartamentoId, ProyectoId, ClienteId);
            validacion.DefinirUbicacion(lugar, plataforma);
            if (horaFin.HasValue)
                validacion.DefinirFin(horaFin.Value);

            Titulo = validacion.Titulo;
            Tipo = validacion.Tipo;
            FechaReunion = validacion.FechaReunion;
            HoraInicio = validacion.HoraInicio;
            HoraFin = validacion.HoraFin;
            Lugar = validacion.Lugar;
            Plataforma = validacion.Plataforma;
            Objetivo = validacion.Objetivo;
        }

        public void RestaurarPersistencia(long id, long creadoPor, int versionFila, EstadoMinuta estado) {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (creadoPor <= 0)
                throw new ArgumentOutOfRangeException(nameof(creadoPor));
            if (versionFila <= 0)
                throw new ArgumentOutOfRangeException(nameof(versionFila));
            if (!Enum.IsDefined(typeof(EstadoMinuta), estado))
                throw new ArgumentOutOfRangeException(nameof(estado));
            IdMinuta = id;
            Creo = creadoPor;
            VersionFila = versionFila;
            Estado = estado;
        }

        private void AsegurarEditable() {
            if (Estado == EstadoMinuta.Aprobada || Estado == EstadoMinuta.Cancelada)
                throw new InvalidOperationException("No se puede modificar una minuta aprobada o cancelada.");
        }

        public void CambiarEstado(EstadoMinuta nuevoEstado) {
            if (!Enum.IsDefined(typeof(EstadoMinuta), nuevoEstado))
                throw new ArgumentOutOfRangeException(nameof(nuevoEstado));
            if (Estado == EstadoMinuta.Aprobada || Estado == EstadoMinuta.Cancelada)
                throw new InvalidOperationException("La minuta aprobada o cancelada es un estado terminal.");
            bool permitido =
                (Estado == EstadoMinuta.Borrador && (nuevoEstado == EstadoMinuta.Preparada || nuevoEstado == EstadoMinuta.Cancelada)) ||
                (Estado == EstadoMinuta.Preparada && (nuevoEstado == EstadoMinuta.EnReunion || nuevoEstado == EstadoMinuta.Cancelada)) ||
                (Estado == EstadoMinuta.EnReunion && (nuevoEstado == EstadoMinuta.PendienteAprobacion || nuevoEstado == EstadoMinuta.Cancelada)) ||
                (Estado == EstadoMinuta.PendienteAprobacion && (nuevoEstado == EstadoMinuta.Aprobada || nuevoEstado == EstadoMinuta.EnReunion || nuevoEstado == EstadoMinuta.Cancelada));
            if (!permitido)
                throw new InvalidOperationException("La transición de " + Estado + " a " + nuevoEstado + " no está permitida.");
            Estado = nuevoEstado;
        }

        private static string NormalizarOpcional(string valor) {
            return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
        }
        #endregion
    }
}
