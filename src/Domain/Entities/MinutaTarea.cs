using System;

namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaTarea {
        public long Id { get; set; }
        public long MinutaId { get; set; }
        public long? AcuerdoId { get; set; }
        public int? AcuerdoOrdenSeleccionado { get; set; }
        public string Descripcion { get; set; }
        public long ResponsableId { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string Evidencia { get; set; }
        public long CreadoPor { get; set; }
    }
}
