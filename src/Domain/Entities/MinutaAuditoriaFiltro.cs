using System;

namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaAuditoriaFiltro {
        public string FolioOIdMinuta { get; set; }
        public int? UsuarioId { get; set; }
        public string Accion { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public int Limite { get; set; }
    }
}
