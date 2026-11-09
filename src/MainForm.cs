using System;
using System.Drawing;
using System.Windows.Forms;
using SqlSugar;
using Jaeger.Minutas.Aplication.DependencyInjection;
using Jaeger.Minutas.DataAccess.Datbase;
using Jaeger.Minutas.Properties;
using Jaeger.Minutas.UserInterface;
using Jaeger.Minutas.Aplication.Services;

namespace Jaeger.Minutas {
    public partial class MainForm : Form {
        private readonly SqlSugarClient _db;
        private readonly MinutasComposition _composition;
        private readonly NumericUpDown _minutaId;
        private readonly Button _eliminar;
        
        public MainForm() {
            InitializeComponent();
            _db = SqlSugarFactory.Crear(Settings.Default.ConnectionString);
            _composition = new MinutasComposition(_db);
            var titulo = new Label { Text = "Control y seguimiento de minutas", AutoSize = true, Font = new Font(Font.FontFamily, 14, FontStyle.Bold) };
            this.lblEstado.Text = EstadoSesion() ;
            this.opSalir.Click += (s, e) => Close();
            FormClosed += (s, e) => _db.Dispose();
            ActualizarEstadoSesion();
        }

        private string EstadoSesion() {
            SesionActual.UsuarioId = 1;
            return SesionActual.UsuarioId > 0 ? "Usuario corporativo: " + SesionActual.UsuarioId : "Sesión corporativa pendiente de integrar";
        }

        private void ActualizarEstadoSesion() {
            bool habilitada = SesionActual.UsuarioId > 0;
            opMinutas.Enabled = habilitada;
            this.Text = EstadoSesion();
        }

        private async System.Threading.Tasks.Task EliminarMinutaAsync(long id) {
            if (SesionActual.UsuarioId <= 0) { MessageBox.Show("No hay usuario autenticado.", "Minutas"); return; }
            DialogResult confirmar = MessageBox.Show(
                "La minuta se marcará como eliminada y dejará de aparecer en las consultas. Esta operación no elimina físicamente sus datos. ¿Desea continuar?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes)
                return;
            _eliminar.Enabled = false;
            try {
                await _composition.AdministrarMinutaService.EliminarAsync(id);
                MessageBox.Show("La minuta se marcó como eliminada.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (InvalidOperationException ex) {
                MessageBox.Show(ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (Exception) {
                MessageBox.Show("No fue posible eliminar la minuta. Verifique la conexión y el registro de errores.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally { _eliminar.Enabled = SesionActual.UsuarioId > 0; }
        }

        private void AbrirAuditoria() {
            if (SesionActual.UsuarioId <= 0) { MessageBox.Show("No hay usuario autenticado.", "Minutas"); return; }
            using (var form = new MinutaAuditoria1Form(_composition.MinutaAuditoriaService))
                form.ShowDialog(this);
        }

        private void AbrirDetalle(long id) {
            if (SesionActual.UsuarioId <= 0) { MessageBox.Show("No hay usuario autenticado.", "Minutas"); return; }
            try {
                using (var form = new MinutaEditarForm(id, SesionActual.UsuarioId, _composition.MinutaDetalleService, _composition.UsuarioCorporativoService, _composition.ObtenerMinutaService, _composition.AdministrarMinutaService, "ID " + id))
                    form.ShowDialog(this);
            } catch (Exception) {
                MessageBox.Show("No fue posible abrir el detalle. Verifique que la minuta exista y que el esquema de fase 2 esté instalado.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void opMinutas_Click(object sender, EventArgs e) {
            var form = new MinutasForm(_composition, SesionActual.UsuarioId) { MdiParent = this };
            form.Show();
        }
    }
}
