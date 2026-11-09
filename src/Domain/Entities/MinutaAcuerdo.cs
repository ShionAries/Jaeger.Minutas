using System;

namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaAcuerdo {
        public long Id { get; set; }
        public long MinutaId { get; set; }
        public long? TemaId { get; set; }
        public int? TemaOrdenSeleccionado { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public long RegistradoPor { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
