using System.Collections.Generic;

namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaDetalle {
        public MinutaDetalle() {
            Asistentes = new List<MinutaAsistente>();
            Temas = new List<MinutaTema>();
            Acuerdos = new List<MinutaAcuerdo>();
            Tareas = new List<MinutaTarea>();
        }
        public long MinutaId { get; set; }
        public int VersionFila { get; set; }
        public List<MinutaAsistente> Asistentes { get; set; }
        public List<MinutaTema> Temas { get; set; }
        public List<MinutaAcuerdo> Acuerdos { get; set; }
        public List<MinutaTarea> Tareas { get; set; }
    }
}
