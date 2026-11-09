using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaeger.Minutas.Aplication.Services;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.UserInterface {
    public partial class MinutaTrazabilidadForm : Form {
        private readonly long _minutaId;
        private readonly MinutaTrazabilidadService _service;
        private bool _cargando;

        public MinutaTrazabilidadForm(long minutaId, MinutaTrazabilidadService service) {
            InitializeComponent();
            if (minutaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(minutaId));

            _minutaId = minutaId;
            _service = service ?? throw new ArgumentNullException(nameof(service));

            ConfigurarGridMinutas();
            ConfigurarGridTareas();
            _grid.SelectionChanged += async (s, e) => { if (!_cargando) await CargarTareasRelacionadasAsync(); };
            Shown += async (s, e) => await CargarAsync();
        }

        private void ConfigurarGridMinutas() {
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AutoGenerateColumns = false;
            _grid.RowHeadersVisible = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void ConfigurarGridTareas() {
            _tareas.Dock = DockStyle.Fill;
            _tareas.ReadOnly = true;
            _tareas.AllowUserToAddRows = false;
            _tareas.AllowUserToDeleteRows = false;
            _tareas.AutoGenerateColumns = false;
            _tareas.RowHeadersVisible = false;
            _tareas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private async Task CargarAsync() {
            _cargando = true;
            try {
                _grid.DataSource = await _service.ObtenerCadenaAsync(_minutaId);
            } catch (Exception ex) {
                MessageBox.Show("No fue posible consultar la trazabilidad. " + ex.Message, "Trazabilidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            } finally {
                _cargando = false;
            }
            await CargarTareasRelacionadasAsync();
        }
        private async Task CargarTareasRelacionadasAsync() {
            var actual = _grid.CurrentRow == null ? null : _grid.CurrentRow.DataBoundItem as MinutaTrazabilidadNodo;
            long id = actual == null ? _minutaId : actual.MinutaId;
            try {
                _tareas.DataSource = await _service.ObtenerTareasRelacionadasAsync(id);
            } catch (Exception ex) {
                MessageBox.Show("No fue posible cargar el seguimiento de acuerdos. " + ex.Message, "Trazabilidad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
