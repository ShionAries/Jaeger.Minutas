using System;
using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("MNTTA")]
    public sealed class MinutaTareaEntity {
        [SugarColumn(ColumnName = "MNTTA_ID", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        [SugarColumn(ColumnName = "MNTTA_MNTA_ID")]
        public long MinutaId { get; set; }
        [SugarColumn(ColumnName = "MNTTA_MNTAC_ID", IsNullable = true)]
        public long? AcuerdoId { get; set; }
        [SugarColumn(ColumnName = "MNTTA_DESC", ColumnDataType = "text")]
        public string Descripcion { get; set; }
        [SugarColumn(ColumnName = "MNTTA_DRCTR_ID")]
        public long ResponsableId { get; set; }
        [SugarColumn(ColumnName = "MNTTA_FCLIM", ColumnDataType = "date")]
        public DateTime FechaLimite { get; set; }
        [SugarColumn(ColumnName = "MNTTA_ESTADO", Length = 20)]
        public string Estado { get; set; }
        [SugarColumn(ColumnName = "MNTTA_FECFIN", IsNullable = true)]
        public DateTime? FechaFinalizacion { get; set; }
        [SugarColumn(ColumnName = "MNTTA_EVDCIA", ColumnDataType = "text", IsNullable = true)]
        public string Evidencia { get; set; }
        [SugarColumn(ColumnName = "MNTTA_USR_N")]
        public long CreadoPor { get; set; }
        [SugarColumn(ColumnName = "MNTTA_FN", IsOnlyIgnoreUpdate = true)]
        public DateTime FechaCreacion { get; set; }
        [SugarColumn(ColumnName = "MNTTA_FECACT", IsOnlyIgnoreInsert = true)]
        public DateTime FechaActualizacion { get; set; }
    }
}
