using System;
namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaSeguimientoPendiente {
        public long TareaId { get; set; }
        public long? AcuerdoId { get; set; }
        public string Acuerdo { get; set; }
        public string Descripcion { get; set; }
        public long ResponsableId { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Estado { get; set; }
        public bool Seleccionar { get; set; }
        public string Responsable { get; set; }
    }
}
