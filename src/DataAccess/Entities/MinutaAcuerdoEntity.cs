using System;
using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("MNTAC")]
    public sealed class MinutaAcuerdoEntity {
        public MinutaAcuerdoEntity() {
            this.FechaRegistro = DateTime.Now;
        }

        [SugarColumn(ColumnName = "MNTAC_ID", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        [SugarColumn(ColumnName = "MNTAC_MNTA_ID")]
        public long MinutaId { get; set; }
        [SugarColumn(ColumnName = "MNTAC_TEMA_ID", IsNullable = true)]
        public long? TemaId { get; set; }
        [SugarColumn(ColumnName = "MNTAC_CTTPO", Length = 20)]
        public string Tipo { get; set; }
        [SugarColumn(ColumnName = "MNTAC_DESC", ColumnDataType = "text")]
        public string Descripcion { get; set; }
        [SugarColumn(ColumnName = "MNTAC_ORDEN")]
        public int Orden { get; set; }
        [SugarColumn(ColumnName = "MNTAC_USR_N")]
        public long RegistradoPor { get; set; }
        [SugarColumn(ColumnName = "MNTAC_FN", IsOnlyIgnoreUpdate = true)]
        public DateTime FechaRegistro { get; set; }
    }
}
