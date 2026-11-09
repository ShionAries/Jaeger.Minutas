using System.ComponentModel;

namespace Jaeger.Minutas.Domain.Enums {
    public enum EstadoMinutaEnum {
        [Description("Borrador")]
        Borrador = 0,
        [Description("Preparada")]
        Preparada = 1,
        [Description("En reunión")]
        EnReunion = 2,
        [Description("Pendiente Aprobación")]
        PendienteAprobacion = 3,
        [Description("Aprobada")]
        Aprobada = 4,
        [Description("Cancelada")]
        Cancelada = 5
    }
}
