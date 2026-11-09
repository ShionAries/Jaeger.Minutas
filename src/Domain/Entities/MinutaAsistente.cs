namespace Jaeger.Minutas.Domain.Entities {
    /// <summary>
    /// clase modelo para asistentes a la reunion
    /// </summary>
    public sealed class MinutaAsistente {
        /// <summary>
        /// obtener o establecer indice incremental
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// obtener o establecer indice de la minuta
        /// </summary>
        public long IdMinuta { get; set; }

        /// <summary>
        /// obtener o establecer indice del usuario
        /// </summary>
        public long? IdUsuario { get; set; }

        /// <summary>
        /// obtener o establecer nombre del participante
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// obtener o establecer correo electronico
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// obtener o establecer rol dl asistente
        /// </summary>
        public string Rol { get; set; }

        /// <summary>
        /// obtener o establecer el estado de la asistencia
        /// </summary>
        public string EstadoAsistencia { get; set; }
    }
}
