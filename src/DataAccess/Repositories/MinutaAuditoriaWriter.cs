using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SqlSugar;
using Jaeger.Minutas.DataAccess.Entities;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.DataAccess.Repositories {
    internal static class MinutaAuditoriaWriter {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };

        public static string Snapshot(MinutaEntity minuta) {
            if (minuta == null)
                return null;
            var datos = new Dictionary<string, object> {
                { "Id", minuta.Id }, { "Folio", minuta.Folio }, { "Titulo", minuta.Titulo },
                { "Tipo", minuta.Tipo }, { "FechaReunion", minuta.FechaReunion.ToString("yyyy-MM-dd") },
                { "HoraInicio", minuta.HoraInicio.ToString() }, { "HoraFin", minuta.HoraFin.HasValue ? minuta.HoraFin.Value.ToString() : null },
                { "Lugar", minuta.Lugar }, { "Plataforma", minuta.Plataforma }, { "Objetivo", minuta.Objetivo },
                { "Estado", minuta.Estado }, { "VersionFila", minuta.VersionFila }, { "Eliminado", minuta.Eliminado }
            };
            return Serializer.Serialize(datos);
        }

        public static string Snapshot(Minuta minuta) {
            if (minuta == null)
                return null;
            var datos = new Dictionary<string, object> {
                { "Id", minuta.IdMinuta }, { "Folio", minuta.Folio }, { "Titulo", minuta.Titulo },
                { "Tipo", minuta.Tipo }, { "FechaReunion", minuta.FechaReunion.ToString("yyyy-MM-dd") },
                { "HoraInicio", minuta.HoraInicio.ToString() }, { "HoraFin", minuta.HoraFin.HasValue ? minuta.HoraFin.Value.ToString() : null },
                { "Lugar", minuta.Lugar }, { "Plataforma", minuta.Plataforma }, { "Objetivo", minuta.Objetivo },
                { "Estado", minuta.Estado.ToString() }, { "VersionFila", minuta.VersionFila }
            };
            return Serializer.Serialize(datos);
        }

        public static Task RegistrarAsync(SqlSugarClient db, long minutaId, long usuarioId, string accion, string anterior, string nuevo) {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            return db.Ado.ExecuteCommandAsync(
                "INSERT INTO MNTAR (MNTAR_MNTA_ID, MNTAR_DRCTR_ID, MNTAR_ACCION, MNTAR_FM, MNTAR_DANT, MNTAR_DNUE) VALUES (@minutaId, @usuarioId, @accion, CURRENT_TIMESTAMP, @anterior, @nuevo)",
                new SugarParameter("@minutaId", minutaId), new SugarParameter("@usuarioId", usuarioId),
                new SugarParameter("@accion", accion), new SugarParameter("@anterior", (object)anterior ?? DBNull.Value),
                new SugarParameter("@nuevo", (object)nuevo ?? DBNull.Value));
        }
    }
}
