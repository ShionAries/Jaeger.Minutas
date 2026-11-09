using System;

namespace Jaeger.Minutas {
    internal static class Program {
        [STAThread]
        private static void Main() {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Integración obligatoria: asignar SesionActual.UsuarioId desde la sesión corporativa autenticada.
            // Este proyecto de muestra no contiene el formulario de inicio de sesión del sistema anfitrión.
            System.Windows.Forms.Application.Run(new MainForm());
        }
    }
}
