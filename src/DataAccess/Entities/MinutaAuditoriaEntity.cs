using System;
using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("MNTAR")]
    public sealed class MinutaAuditoriaEntity {
        [SugarColumn(ColumnName = "MNTAR_ID", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        [SugarColumn(ColumnName = "MNTAR_MNTA_ID")]
        public long MinutaId { get; set; }
        [SugarColumn(ColumnName = "MNTAR_DRCTR_ID")]
        public int UsuarioId { get; set; }
        [SugarColumn(ColumnName = "MNTAR_ACCION", Length = 40)]
        public string Accion { get; set; }
        [SugarColumn(ColumnName = "MNTAR_FM")]
        public DateTime FechaEvento { get; set; }
        [SugarColumn(ColumnName = "MNTAR_DANT", ColumnDataType = "longtext", IsNullable = true)]
        public string DatosAnteriores { get; set; }
        [SugarColumn(ColumnName = "MNTAR_DNUE", ColumnDataType = "longtext", IsNullable = true)]
        public string DatosNuevos { get; set; }
    }
}
