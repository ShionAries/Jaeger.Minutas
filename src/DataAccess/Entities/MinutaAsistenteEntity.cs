using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("MNTAS")]
    public sealed class MinutaAsistenteEntity {
        [SugarColumn(ColumnName = "MNTAS_ID", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        [SugarColumn(ColumnName = "MNTAS_MNTA_ID")]
        public long MinutaId { get; set; }
        [SugarColumn(ColumnName = "MNTAS_USR_ID", IsNullable = true)]
        public long? UsuarioId { get; set; }
        [SugarColumn(ColumnName = "MNTAS_NOM", Length = 200)]
        public string Nombre { get; set; }
        [SugarColumn(ColumnName = "MNTAS_MAIL", Length = 254, IsNullable = true)]
        public string Correo { get; set; }
        [SugarColumn(ColumnName = "MNTAS_ROL", Length = 100)]
        public string Rol { get; set; }
        [SugarColumn(ColumnName = "MNTAS_STTS_ID", Length = 20)]
        public string EstadoAsistencia { get; set; }
    }
}
