namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaTrazabilidadTarea {
        public string FolioOrigen { get; set; }
        public string FolioDestino { get; set; }
        public string AcuerdoOrigen { get; set; }
        public string TareaOrigen { get; set; }
        public string EstadoOrigen { get; set; }
        public string AcuerdoDestino { get; set; }
        public string TareaDestino { get; set; }
        public string EstadoDestino { get; set; }
    }
}
