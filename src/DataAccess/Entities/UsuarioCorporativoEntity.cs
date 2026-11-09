using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("_user")]
    public sealed class UsuarioCorporativoEntity {
        [SugarColumn(ColumnName = "_user_id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "_user_a")]
        public short Activo { get; set; }

        [SugarColumn(ColumnName = "_user_mail")]
        public string Correo { get; set; }

        [SugarColumn(ColumnName = "_user_nom")]
        public string Nombre { get; set; }
    }
}
