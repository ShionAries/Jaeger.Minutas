namespace Jaeger.Minutas.Domain.Entities {
    public sealed class MinutaTema {
        public long Id { get; set; }
        public long MinutaId { get; set; }
        public int Orden { get; set; }
        public string Tema { get; set; }
        public string Descripcion { get; set; }
        public long? ResponsableId { get; set; }
        public string Estado { get; set; }
    }
}
