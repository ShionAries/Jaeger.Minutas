using System;
namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaTrazabilidadNodo {
        public long MinutaId { get; set; }
        public string Folio { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaReunion { get; set; }
        public string Estado { get; set; }
        public int Nivel { get; set; }
        public string Relacion { get; set; }
        public bool Eliminada { get; set; }
    }
}
