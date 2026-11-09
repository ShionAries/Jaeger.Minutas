using System;
using SqlSugar;
namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("MNTRZ")]
    public sealed class MinutaRelacionEntity {
        [SugarColumn(ColumnName = "MNTRZ_ID", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        
        [SugarColumn(ColumnName = "MNTRZ_ORIGEN_ID")]
        public long MinutaOrigenId { get; set; }
        
        [SugarColumn(ColumnName = "MNTRZ_DESTINO_ID")]
        public long MinutaDestinoId { get; set; }
        
        [SugarColumn(ColumnName = "MNTRZ_USR_ID")]
        public long UsuarioId { get; set; }
        
        [SugarColumn(ColumnName = "MNTRZ_FN")]
        public DateTime FechaCreacion { get; set; }
    }
}
