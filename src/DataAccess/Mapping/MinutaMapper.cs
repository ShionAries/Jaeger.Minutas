using System;
using Jaeger.Minutas.DataAccess.Entities;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;

namespace Jaeger.Minutas.DataAccess.Mapping {
    internal static class MinutaMapper {
        public static MinutaEntity ToEntity(Minuta minuta, long usuarioId) {
            return new MinutaEntity {
                Folio = minuta.Folio,
                Titulo = minuta.Titulo,
                Tipo = minuta.Tipo,
                FechaReunion = minuta.FechaReunion,
                HoraInicio = minuta.HoraInicio,
                HoraFin = minuta.HoraFin,
                Lugar = minuta.Lugar,
                Plataforma = minuta.Plataforma,
                Objetivo = minuta.Objetivo,
                DepartamentoId = minuta.DepartamentoId,
                ProyectoId = minuta.ProyectoId,
                ClienteId = minuta.ClienteId,
                Estado = minuta.Estado.ToString(),
                ProximaFecha = minuta.ProximaFecha,
                ProximaLugar = minuta.ProximaLugar,
                CreadoPor = usuarioId,
                VersionFila = 1,
                Eliminado = false
            };
        }

        public static Minuta ToDomain(MinutaEntity entity) {
            if (entity == null)
                return null;

            var minuta = new Minuta(
                entity.Folio,
                entity.Titulo,
                entity.FechaReunion,
                entity.HoraInicio,
                entity.Objetivo);

            minuta.DefinirClasificacion(
                entity.Tipo,
                entity.DepartamentoId,
                entity.ProyectoId,
                entity.ClienteId);
            minuta.DefinirUbicacion(entity.Lugar, entity.Plataforma);

            if (entity.HoraFin.HasValue)
                minuta.DefinirFin(entity.HoraFin.Value);

            minuta.DefinirProximaReunion(entity.ProximaFecha, entity.ProximaLugar);
            minuta.RestaurarPersistencia(entity.Id, entity.CreadoPor, entity.VersionFila, ParseEstado(entity.Estado));
            return minuta;
        }

        private static EstadoMinuta ParseEstado(string valor) {
            EstadoMinuta estado;
            if (!Enum.TryParse(valor, true, out estado) ||
                !Enum.IsDefined(typeof(EstadoMinuta), estado))
                throw new InvalidOperationException("La minuta contiene un estado desconocido.");
            return estado;
        }
    }
}
