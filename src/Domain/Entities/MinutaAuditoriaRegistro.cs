using System;

namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaAuditoriaRegistro {
        public long Id { get; set; }
        public long MinutaId { get; set; }
        public string Folio { get; set; }
        public string TituloMinuta { get; set; }
        public long UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public DateTime FechaEvento { get; set; }
        public string DatosAnteriores { get; set; }
        public string DatosNuevos { get; set; }
    }
}
