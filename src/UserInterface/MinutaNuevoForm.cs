using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaeger.Minutas.Aplication.Services;
using Jaeger.Minutas.Application.Contracts;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;

namespace Jaeger.Minutas.UserInterface {
    public partial class MinutaNuevoForm : Form {
        private readonly IMinutaService _service;
        private readonly UsuarioCorporativoService _usuariosService;
        private readonly long _usuarioId;
        private List<UsuarioCorporativo> _usuarios = new List<UsuarioCorporativo>();
        private BindingList<MinutaAsistente> _listaAsistentes = new BindingList<MinutaAsistente>();
        private bool _eventosConfigurados;
        private bool _actualizandoAsistente;

        public long MinutaIdCreada { get; private set; }

        public MinutaNuevoForm(IMinutaService service, UsuarioCorporativoService usuariosService, long usuarioId) {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _usuariosService = usuariosService ?? throw new ArgumentNullException(nameof(usuariosService));
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));

            _usuarioId = usuarioId;
            InitializeComponent();
            ConstruirInterfaz();
            Shown += async (s, e) => await CargarAsync();
        }

        private void ConstruirInterfaz() {
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Items.AddRange(new object[] { "General", "Comité", "Diaria", "Semanal", "Cliente" });
            cmbTipo.SelectedIndex = 0;

            dtpFechaReunion.Format = DateTimePickerFormat.Short;
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.ShowUpDown = true;
            dtpHora.Value = DateTime.Today.AddHours(9);

            txtObjetivo.ScrollBars = ScrollBars.Vertical;
            cboStatus.DataSource = ((EstadoMinuta[])Enum.GetValues(typeof(EstadoMinuta)))
                .Select(x => new StatusModel((int)x, x.ToString())).ToList();
            cboStatus.DisplayMember = "Descripcion";
            cboStatus.ValueMember = "Id";
            cboStatus.Enabled = false;

            txtFolio.ReadOnly = true;
            txtFolio.Text = "(automático)";
            btnGuardar.Text = "Crear minuta";
            btnGuardar.AutoSize = true;
            btnGuardar.Click += async (s, e) => await GuardarAsync();
            btnCerrar.Click += (s, e) => Close();

            TAsistenteAgregar.Click += (s, e) => AgregarAsistente();
            TAsistenteRemover.Click += (s, e) => RemoverAsistente();
        }

        private async Task CargarAsync() {
            btnGuardar.Enabled = false;
            TAsistenteAgregar.Enabled = false;
            try {
                Text = "Nueva minuta";
                _usuarios = await _usuariosService.ObtenerActivosAsync().ConfigureAwait(true);
                ConfigurarColumnas();

                _listaAsistentes = new BindingList<MinutaAsistente>();
                _asistentes.DataSource = _listaAsistentes;

                if (!_eventosConfigurados) {
                    _asistentes.CellValueChanged += AsistenteSeleccionado;
                    _asistentes.CurrentCellDirtyStateChanged += (s, e) => {
                        if (_asistentes.IsCurrentCellDirty)
                            _asistentes.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    };
                    _asistentes.DataError += (s, e) => {
                        e.ThrowException = false;
                        e.Cancel = true;
                    };
                    _asistentes.RowsRemoved += (s, e) => ActualizarEstadoBotonRemover();
                    _eventosConfigurados = true;
                }

                if (_usuarios.Count == 0) {
                    MessageBox.Show(
                        "No hay usuarios activos en el catálogo corporativo. No es posible agregar asistentes.",
                        "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                TAsistenteAgregar.Enabled = _usuarios.Count > 0;
                ActualizarEstadoBotonRemover();
            } catch (Exception) {
                MessageBox.Show(
                    "No fue posible cargar el catálogo de usuarios corporativos. Verifique la conexión y la configuración.",
                    "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                btnGuardar.Enabled = true;
            }
        }

        private void ConfigurarColumnas() {
            _asistentes.AutoGenerateColumns = false;
            _asistentes.Columns.Clear();
            _asistentes.AllowUserToAddRows = false;
            _asistentes.AllowUserToDeleteRows = false;
            _asistentes.MultiSelect = false;
            _asistentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _asistentes.RowHeadersVisible = false;
            _asistentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _asistentes.EditMode = DataGridViewEditMode.EditOnEnter;

            var usuario = new DataGridViewComboBoxColumn {
                Name = "IdUsuario",
                DataPropertyName = "IdUsuario",
                HeaderText = "Usuario corporativo",
                DataSource = _usuarios,
                DisplayMember = "NombreMostrar",
                ValueMember = "Id",
                ValueType = typeof(long),
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                FlatStyle = FlatStyle.Flat,
                FillWeight = 155,
                MinimumWidth = 150,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            usuario.DefaultCellStyle.NullValue = null;
            _asistentes.Columns.Add(usuario);

            _asistentes.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                ReadOnly = true,
                FillWeight = 130,
                MinimumWidth = 100
            });
            _asistentes.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Correo",
                DataPropertyName = "Correo",
                HeaderText = "Correo",
                ReadOnly = true,
                FillWeight = 150,
                MinimumWidth = 100
            });
            _asistentes.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Rol",
                DataPropertyName = "Rol",
                HeaderText = "Rol en la reunión",
                FillWeight = 100,
                MinimumWidth = 90,
                MaxInputLength = 100
            });

            var estado = new DataGridViewComboBoxColumn {
                Name = "EstadoAsistencia",
                DataPropertyName = "EstadoAsistencia",
                HeaderText = "Asistencia",
                DataSource = new List<string> { "Convocado", "Confirmado", "Asistió", "No asistió", "Canceló" },
                FillWeight = 90,
                MinimumWidth = 90,
                FlatStyle = FlatStyle.Flat,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            _asistentes.Columns.Add(estado);
        }

        private void AgregarAsistente() {
            if (_usuarios.Count == 0)
                return;

            if (_listaAsistentes.Any(x => !x.IdUsuario.HasValue)) {
                MessageBox.Show(
                    "Seleccione el usuario de la fila pendiente antes de agregar otro asistente.",
                    "Asistentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _listaAsistentes.Add(new MinutaAsistente {
                Rol = "Asistente",
                EstadoAsistencia = "Convocado"
            });

            int fila = _asistentes.Rows.Count - 1;
            if (fila >= 0) {
                _asistentes.CurrentCell = _asistentes.Rows[fila].Cells["IdUsuario"];
                _asistentes.BeginEdit(true);
                _asistentes.Rows[fila].Cells["IdUsuario"].Selected = true;
            }
            ActualizarEstadoBotonRemover();
        }

        private void RemoverAsistente() {
            if (_asistentes.CurrentRow == null)
                return;

            var asistente = _asistentes.CurrentRow.DataBoundItem as MinutaAsistente;
            if (asistente == null)
                return;

            _listaAsistentes.Remove(asistente);
            ActualizarEstadoBotonRemover();
        }

        private void ActualizarEstadoBotonRemover() {
            TAsistenteRemover.Enabled = _asistentes.CurrentRow != null
                && _asistentes.CurrentRow.DataBoundItem is MinutaAsistente;
        }

        private void AsistenteSeleccionado(object sender, DataGridViewCellEventArgs e) {
            if (_actualizandoAsistente || e.RowIndex < 0 || e.ColumnIndex < 0
                || _asistentes.Columns[e.ColumnIndex].Name != "IdUsuario")
                return;

            var asistente = _asistentes.Rows[e.RowIndex].DataBoundItem as MinutaAsistente;
            if (asistente == null || !asistente.IdUsuario.HasValue)
                return;

            UsuarioCorporativo usuario = _usuarios.Find(x => x.Id == asistente.IdUsuario.Value);
            if (usuario == null)
                return;

            bool duplicado = _listaAsistentes.Any(x => !ReferenceEquals(x, asistente)
                && x.IdUsuario.HasValue && x.IdUsuario.Value == usuario.Id);
            if (duplicado) {
                _actualizandoAsistente = true;
                try {
                    asistente.IdUsuario = null;
                    asistente.Nombre = null;
                    asistente.Correo = null;
                    _asistentes.Refresh();
                } finally {
                    _actualizandoAsistente = false;
                }

                MessageBox.Show(
                    "El usuario seleccionado ya está agregado como asistente.",
                    "Asistentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _actualizandoAsistente = true;
            try {
                asistente.Nombre = usuario.NombreMostrar;
                asistente.Correo = usuario.Correo;
                if (string.IsNullOrWhiteSpace(asistente.Rol))
                    asistente.Rol = "Asistente";
                if (string.IsNullOrWhiteSpace(asistente.EstadoAsistencia))
                    asistente.EstadoAsistencia = "Convocado";
                _asistentes.Refresh();
                if (e.RowIndex < _asistentes.Rows.Count && _asistentes.Columns.Contains("Rol"))
                    _asistentes.CurrentCell = _asistentes.Rows[e.RowIndex].Cells["Rol"];
            } finally {
                _actualizandoAsistente = false;
            }
        }

        private async Task GuardarAsync() {
            if (btnGuardar.Enabled == false)
                return;

            btnGuardar.Enabled = false;
            try {
                _asistentes.EndEdit();
                ValidateChildren();

                if (string.IsNullOrWhiteSpace(txtTitulo.Text))
                    throw new ArgumentException("Capture el título de la minuta.");
                if (string.IsNullOrWhiteSpace(txtObjetivo.Text))
                    throw new ArgumentException("Capture el objetivo de la minuta.");

                if (_listaAsistentes.Any(x => !x.IdUsuario.HasValue || x.IdUsuario.Value <= 0)) {
                    throw new ArgumentException("Seleccione un usuario corporativo en cada fila de asistentes o elimine las filas incompletas.");
                }

                var ids = _listaAsistentes.Select(x => x.IdUsuario.Value).ToList();
                if (ids.Distinct().Count() != ids.Count)
                    throw new ArgumentException("No puede agregar dos veces al mismo usuario como asistente.");

                foreach (var asistente in _listaAsistentes) {
                    if (string.IsNullOrWhiteSpace(asistente.Rol))
                        asistente.Rol = "Asistente";
                    if (string.IsNullOrWhiteSpace(asistente.EstadoAsistencia))
                        asistente.EstadoAsistencia = "Convocado";
                }

                var minuta = new MinutaDetail(
                    txtTitulo.Text.Trim(),
                    dtpFechaReunion.Value.Date,
                    dtpHora.Value.TimeOfDay,
                    txtObjetivo.Text.Trim());

                minuta.Asistentes = _listaAsistentes.ToList();
                minuta.DefinirClasificacion(Convert.ToString(cmbTipo.SelectedItem), null, null, null);
                minuta.DefinirUbicacion(txtUbicacion.Text, txtPlataforma.Text);

                MinutaDetail creada = await _service.GuardarAsync(minuta);
                MinutaIdCreada = creada.IdMinuta;
                DialogResult = DialogResult.OK;
                Close();
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (InvalidOperationException ex) {
                MessageBox.Show(ex.Message, "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (Exception) {
                MessageBox.Show(
                    "No fue posible crear la minuta ni guardar sus asistentes. Verifique la conexión y el registro de errores.",
                    "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                btnGuardar.Enabled = true;
            }
        }
    }
}
