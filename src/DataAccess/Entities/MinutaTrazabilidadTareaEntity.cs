using SqlSugar;
namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("MNTRZT")]
    public sealed class MinutaTrazabilidadTareaEntity {
        [SugarColumn(ColumnName = "MNTRZT_ID", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_RELACION_ID")]
        public long RelacionId { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_TAREA_ORIGEN_ID")]
        public long TareaOrigenId { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_TAREA_DESTINO_ID")] 
        public long TareaDestinoId { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_ACUERDO_ORIGEN_ID", IsNullable = true)] 
        public long? AcuerdoOrigenId { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_ACUERDO_DESTINO_ID", IsNullable = true)]
        public long? AcuerdoDestinoId { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_TAREA_ORIGEN_DESC", ColumnDataType = "text")] 
        public string TareaOrigenDescripcion { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_TAREA_DESTINO_DESC", ColumnDataType = "text")] 
        public string TareaDestinoDescripcion { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_ACUERDO_ORIGEN_DESC", ColumnDataType = "text", IsNullable = true)]
        public string AcuerdoOrigenDescripcion { get; set; }
        [SugarColumn(ColumnName = "MNTRZT_ACUERDO_DESTINO_DESC", ColumnDataType = "text", IsNullable = true)]
        public string AcuerdoDestinoDescripcion { get; set; }
    }
}
