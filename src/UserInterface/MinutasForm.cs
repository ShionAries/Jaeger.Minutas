using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaeger.Minutas.Aplication.DependencyInjection;
using Jaeger.Minutas.Aplication.Services;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.UserInterface {
    public partial class MinutasForm : Form {
        private readonly MinutasComposition _composition;
        private long _usuarioId;

        public MinutasForm(MinutasComposition composition, long usuarioId) {
            _composition = composition ?? throw new ArgumentNullException(nameof(composition));
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));
            _usuarioId = usuarioId;
            InitializeComponent();
            
            CrearColumnas();
            this.btnActualizar.Click += async (sender, e) => await this.btnActualizar_ClickAsync(sender, e);
            Shown += async (s, e) => await this.btnActualizar_ClickAsync(s, e);
            var btnImprimir = new ToolStripButton("Imprimir minuta");
            btnImprimir.Click += async (s, e) => await ImprimirMinutaAsync();
            ToolBar.Items.Insert(Math.Max(0, ToolBar.Items.IndexOf(btnActualizar)), btnImprimir);
        }

        private void CrearColumnas() {
            // Crear las columnas del DataGridView
            gridMinutas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            gridMinutas.AutoGenerateColumns = false;
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdMinuta", HeaderText = "ID", DataPropertyName = "IdMinuta", Visible = false });
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Folio", HeaderText = "Folio", DataPropertyName = "Folio", MinimumWidth = 85 });
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Titulo", HeaderText = "Título", DataPropertyName = "Titulo", MinimumWidth = 185, AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells });
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "Fecha", DataPropertyName = "FechaReunion" });
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoraInicio", HeaderText = "Hora de Inicio", DataPropertyName = "HoraInicio", MinimumWidth = 85, AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells });
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Lugar", HeaderText = "Lugar", DataPropertyName = "Lugar" });
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Plataforma", HeaderText = "Plataforma", DataPropertyName = "Plataforma" });
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Objetivo", HeaderText = "Objetivo", DataPropertyName = "Objetivo" });
            gridMinutas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo", HeaderText = "Tipo", DataPropertyName = "Tipo" });
        }

        private void btnNuevo_Click(object sender, EventArgs e) {
            if (SesionActual.UsuarioId <= 0) {
                MessageBox.Show("No hay usuario autenticado.", "Minutas");
                return;
            }

            using (var form = new MinutaNuevoForm(_composition.MinutaService, _composition.UsuarioCorporativoService, SesionActual.UsuarioId)) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    AbrirDetalle(form.MinutaIdCreada);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e) {
            if (this.gridMinutas.CurrentRow == null) {
                MessageBox.Show("Seleccione una minuta para editar.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var minutaId = this.gridMinutas.CurrentRow.DataBoundItem is Minuta minuta ? minuta.IdMinuta : 0;
            AbrirDetalle(minutaId);
        }

        private async void btnSeguimiento_Click(object sender, EventArgs e) {
            if (gridMinutas.CurrentRow == null || !(gridMinutas.CurrentRow.DataBoundItem is Minuta origen)) {
                MessageBox.Show("Seleccione una minuta aprobada para crear su seguimiento.", "Seguimiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (origen.Estado != Jaeger.Minutas.Domain.Enums.EstadoMinuta.Aprobada) {
                MessageBox.Show("Solo se puede crear seguimiento a partir de una minuta aprobada.", "Seguimiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try {
                using (var form = new MinutaSeguimientoForm(origen, _composition.MinutaTrazabilidadService, SesionActual.UsuarioId)) {
                    if (form.ShowDialog(this) == DialogResult.OK) {
                        await btnActualizar_ClickAsync(this, EventArgs.Empty);
                        AbrirDetalle(form.MinutaIdCreada);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("No fue posible iniciar el seguimiento. " + ex.Message, "Seguimiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTrazabilidad_Click(object sender, EventArgs e) {
            if (gridMinutas.CurrentRow == null || !(gridMinutas.CurrentRow.DataBoundItem is Minuta minuta)) {
                MessageBox.Show("Seleccione una minuta para consultar su trazabilidad.", "Trazabilidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try {
                using (var form = new MinutaTrazabilidadForm(minuta.IdMinuta, _composition.MinutaTrazabilidadService))
                    form.ShowDialog(this);
            } catch (Exception ex) {
                MessageBox.Show("No fue posible abrir la trazabilidad. " + ex.Message, "Trazabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e) {
            if (this.gridMinutas.CurrentRow == null) {
                MessageBox.Show("Seleccione una minuta para editar.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var minutaId = this.gridMinutas.CurrentRow.DataBoundItem is Minuta minuta ? minuta.IdMinuta : 0;
            await EliminarMinutaAsync(minutaId);
        }

        private async Task btnActualizar_ClickAsync(object sender, EventArgs e) {
            var minutas = await this._composition.CrearMinutaService.ObtenerMinutasAsync();
            this.gridMinutas.DataSource = minutas;
        }

        private void btnAuditar_Click(object sender, EventArgs e) {
            this.AbrirAuditoria();
        }

        private void btnCerrar_Click(object sender, EventArgs e) {
            this.Close();
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

        private async System.Threading.Tasks.Task EliminarMinutaAsync(long id) {
            if (SesionActual.UsuarioId <= 0) { MessageBox.Show("No hay usuario autenticado.", "Minutas"); return; }
            DialogResult confirmar = MessageBox.Show(
                "La minuta se marcará como eliminada y dejará de aparecer en las consultas. Esta operación no elimina físicamente sus datos. ¿Desea continuar?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes)
                return;
            this.btnEliminar.Enabled = false;
            try {
                await _composition.AdministrarMinutaService.EliminarAsync(id);
                MessageBox.Show("La minuta se marcó como eliminada.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (InvalidOperationException ex) {
                MessageBox.Show(ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (Exception) {
                MessageBox.Show("No fue posible eliminar la minuta. Verifique la conexión y el registro de errores.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                this.btnEliminar.Enabled = SesionActual.UsuarioId > 0;
            }
        }

        private void AbrirAuditoria() {
            if (SesionActual.UsuarioId <= 0) { MessageBox.Show("No hay usuario autenticado.", "Minutas"); return; }
            using (var form = new MinutaAuditoria1Form(_composition.MinutaAuditoriaService))
                form.ShowDialog(this);
        }
        private async Task ImprimirMinutaAsync() {
            if (gridMinutas.CurrentRow == null || !(gridMinutas.CurrentRow.DataBoundItem is Minuta minuta)) {
                MessageBox.Show("Seleccione una minuta para imprimir.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try {
                await MinutaImpresion.MostrarAsync(this, _composition, minuta.IdMinuta);
            } catch (Exception ex) {
                MessageBox.Show("No fue posible preparar la impresión de la minuta. " + ex.Message, "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
