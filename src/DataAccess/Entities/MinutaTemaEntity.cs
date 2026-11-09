using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("MNTTE")]
    public sealed class MinutaTemaEntity {
        [SugarColumn(ColumnName = "MNTTE_ID", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        [SugarColumn(ColumnName = "MNTTE_MNTA_ID")]
        public long MinutaId { get; set; }
        [SugarColumn(ColumnName = "MNTTE_ORDEN")]
        public int Orden { get; set; }
        [SugarColumn(ColumnName = "MNTTE_TEMA", Length = 250)]
        public string Tema { get; set; }
        [SugarColumn(ColumnName = "MNTTE_DESC", ColumnDataType = "text", IsNullable = true)]
        public string Descripcion { get; set; }
        [SugarColumn(ColumnName = "MNTTE_DRCTR_ID", IsNullable = true)]
        public long? ResponsableId { get; set; }
        [SugarColumn(ColumnName = "MNTTE_STTS_ID", Length = 20)]
        public string Estado { get; set; }
    }
}
