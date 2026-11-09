using System;

namespace Jaeger.Minutas.Domain.Entities {
    public sealed class UsuarioCorporativo {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public string NombreMostrar {
            get { return string.IsNullOrWhiteSpace(Nombre) ? "Usuario " + Id : Nombre.Trim(); }
        }
    }
}
