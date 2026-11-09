using System;
using System.Collections.Generic;

namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaDetail : Minuta {
        public MinutaDetail(string folio, string titulo, DateTime fechaReunion, TimeSpan horaInicio, string objetivo) : base(folio, titulo, fechaReunion, horaInicio, objetivo) {
            this.Asistentes = new List<MinutaAsistente>();
        }

        public MinutaDetail(string titulo, DateTime fechaReunion, TimeSpan horaInicio, string objetivo) : base(titulo, fechaReunion, horaInicio, objetivo) {
            this.Asistentes = new List<MinutaAsistente>();
        }

        public List<MinutaAsistente> Asistentes { get; set; }
    }
}
