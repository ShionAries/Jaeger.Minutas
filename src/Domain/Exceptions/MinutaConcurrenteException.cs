using System;

namespace Jaeger.Minutas.Domain.Exceptions {
    public sealed class MinutaConcurrenteException : Exception {
        public MinutaConcurrenteException()
            : base("La versión de la minuta ya no coincide con la versión cargada.") {
        }
    }
}
