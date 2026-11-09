using System;
using SqlSugar;

namespace Jaeger.Minutas.DataAccess.Entities {
    [SugarTable("MNTA")]
    public sealed class MinutaEntity {
        [SugarColumn(ColumnName = "MNTA_ID", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// obtener o establecer folio de la minuta, el cual es unico y no puede repetirse
        /// </summary>
        [SugarColumn(ColumnName = "MNTA_FOLIO", Length = 30)]
        public string Folio { get; set; }

        /// <summary>
        /// obtener o establecer el titulo de la reunion
        /// </summary>
        [SugarColumn(ColumnName = "MNTA_TTLO", Length = 200)]
        public string Titulo { get; set; }

        [SugarColumn(ColumnName = "MNTA_CTTP_ID", Length = 30)]
        public string Tipo { get; set; }

        [SugarColumn(ColumnName = "MNTA_FCREU", ColumnDataType = "date")]
        public DateTime FechaReunion { get; set; }

        [SugarColumn(ColumnName = "MNTA_HRINI", ColumnDataType = "time")]
        public TimeSpan HoraInicio { get; set; }

        [SugarColumn(ColumnName = "MNTA_HRFIN", ColumnDataType = "time", IsNullable = true)]
        public TimeSpan? HoraFin { get; set; }

        [SugarColumn(ColumnName = "MNTA_UBICA", Length = 250, IsNullable = true)]
        public string Lugar { get; set; }

        [SugarColumn(ColumnName = "MNTA_PLTFRMA", Length = 100, IsNullable = true)]
        public string Plataforma { get; set; }

        [SugarColumn(ColumnName = "MNTA_OBJTVO", ColumnDataType = "text")]
        public string Objetivo { get; set; }

        [SugarColumn(ColumnName = "MNTA_CTDPTO_ID", IsNullable = true)]
        public long? DepartamentoId { get; set; }

        [SugarColumn(ColumnName = "MNTA_PRYCT_ID", IsNullable = true)]
        public long? ProyectoId { get; set; }

        [SugarColumn(ColumnName = "MNTA_DRCTR_ID", IsNullable = true)]
        public long? ClienteId { get; set; }

        [SugarColumn(ColumnName = "MNTA_STTS_ID", Length = 30)]
        public string Estado { get; set; }

        [SugarColumn(ColumnName = "MNTA_FCPRO", IsNullable = true)]
        public DateTime? ProximaFecha { get; set; }

        [SugarColumn(ColumnName = "MNTA_UBICAP", Length = 250, IsNullable = true)]
        public string ProximaLugar { get; set; }

        [SugarColumn(ColumnName = "MNTA_USR_ID")]
        public long CreadoPor { get; set; }

        [SugarColumn(ColumnName = "MNTA_FN", IsOnlyIgnoreUpdate = true)]
        public DateTime FechaNuevo { get; set; }

        [SugarColumn(ColumnName = "MNTA_FCACT", IsOnlyIgnoreInsert = true)]
        public DateTime FechaActualizacion { get; set; }

        [SugarColumn(ColumnName = "MNTA_VERFIL")]
        public int VersionFila { get; set; }

        [SugarColumn(ColumnName = "MNTA_A")]
        public bool Eliminado { get; set; }
    }
}
