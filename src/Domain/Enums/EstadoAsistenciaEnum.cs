using System.ComponentModel;

namespace Jaeger.Minutas.Domain.Enums {
    public enum EstadoAsistenciaEnum {
        [Description("Convocado")]
        Convocado,
        [Description("Confirmado")]
        Confirmado,
        [Description("Asistió")]
        Asistió,
        [Description("No asistió")]
        No_asistió,
        [Description("Canceló")]
        Canceló
    }
}
