namespace Jaeger.Minutas.Domain.Entities {
    /// <summary>
    /// clase base model generica
    /// </summary>
    public class StatusModel {
        /// <summary>
        /// constructor
        /// </summary>
        public StatusModel() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">indice del elemento</param>
        /// <param name="descripcion">descripcion del elemento</param>
        public StatusModel(int id, string descripcion) {
            this.Id = id;
            this.Descripcion = descripcion;
        }

        /// <summary>
        /// obtener o establecer el indice del elemento
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// obtener o establecer la descripcion del elemento
        /// </summary>
        public string Descripcion { get; set; }

        public virtual string Descriptor {
            get { return string.Format("{0}: {1}", this.Id.ToString("00"), this.Descripcion); }
        }
    }
}
