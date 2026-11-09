using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaeger.Minutas.Aplication.Services;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;

namespace Jaeger.Minutas.UserInterface {
    public partial class MinutaSeguimientoForm : Form {
        private readonly Minuta _origen;
        private readonly MinutaTrazabilidadService _service;
        private readonly long _usuarioId;
        private List<MinutaSeguimientoPendiente> _tareas = new List<MinutaSeguimientoPendiente>();
        public long MinutaIdCreada { get; private set; }

        public MinutaSeguimientoForm(Minuta origen, MinutaTrazabilidadService service, long usuarioId) {
            _origen = origen ?? throw new ArgumentNullException(nameof(origen));
            _service = service ?? throw new ArgumentNullException(nameof(service));

            InitializeComponent();

            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));
            if (_origen.Estado != EstadoMinuta.Aprobada)
                throw new InvalidOperationException("Solo puede crearse seguimiento a partir de una minuta aprobada.");
            _usuarioId = usuarioId;
            ConstruirInterfaz();
            Shown += async (s, e) => await CargarPendientesAsync();
        }

        private void ConstruirInterfaz() {
            Text = "Crear minuta de seguimiento - " + _origen.Folio;
            _titulo.Text = "Seguimiento: " + _origen.Titulo;

            _objetivo.Multiline = true;
            _objetivo.ScrollBars = ScrollBars.Vertical;
            _objetivo.Text = "Dar seguimiento a los acuerdos pendientes de la minuta " + _origen.Folio + ". " + _origen.Objetivo;

            _lugar.Text = _origen.Lugar;

            _plataforma.Text = _origen.Plataforma;

            _grid.AutoGenerateColumns = false;
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.RowHeadersVisible = false;
            _grid.CurrentCellDirtyStateChanged += (s, e) => {
                if (_grid.IsCurrentCellDirty)
                    _grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            _crear.Text = "Crear seguimiento";
            _crear.Click += async (s, e) => await CrearAsync();
            _cancelar.Text = "Cancelar";
            _cancelar.Click += (s, e) => Close();
        }

        private async Task CargarPendientesAsync() {
            _crear.Enabled = false;
            try {
                _tareas = await _service.ObtenerPendientesAsync(_origen.IdMinuta);
                _grid.DataSource = _tareas;
                if (_tareas.Count == 0) {
                    MessageBox.Show("La minuta aprobada no tiene tareas pendientes o no concluidas. Puede crear el seguimiento sin trasladar tareas.", "Seguimiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show("No fue posible cargar las tareas pendientes. " + ex.Message, "Seguimiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            } finally { _crear.Enabled = true; }
        }

        private async Task CrearAsync() {
            _grid.EndEdit();
            _crear.Enabled = false;
            try {
                if (string.IsNullOrWhiteSpace(_titulo.Text))
                    throw new ArgumentException("Capture el título de la nueva minuta.");
                if (string.IsNullOrWhiteSpace(_objetivo.Text))
                    throw new ArgumentException("Capture el objetivo de la nueva minuta.");
                var minuta = new Minuta(_titulo.Text.Trim(), _fecha.Value.Date, _hora.Value.TimeOfDay, _objetivo.Text.Trim());
                minuta.DefinirClasificacion(_origen.Tipo, _origen.DepartamentoId, _origen.ProyectoId, _origen.ClienteId);
                minuta.DefinirUbicacion(_lugar.Text, _plataforma.Text);
                var seleccionadas = _tareas.Where(x => x.Seleccionar).Select(x => x.TareaId).ToList();
                MinutaIdCreada = await _service.CrearSeguimientoAsync(_origen.IdMinuta, minuta, seleccionadas, _usuarioId);
                MessageBox.Show("La minuta de seguimiento se creó correctamente. Folio: MIN-" + MinutaIdCreada.ToString("D6"), "Seguimiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (InvalidOperationException ex) {
                MessageBox.Show(ex.Message, "Seguimiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (Exception) {
                MessageBox.Show("No fue posible crear el seguimiento. La operación se revirtió si ocurrió un error durante la transacción.", "Seguimiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                _crear.Enabled = true;
            }
        }
    }
}
