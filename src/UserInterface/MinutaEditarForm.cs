using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaeger.Minutas.Aplication.Services;
using Jaeger.Minutas.Domain.Entities;
using Jaeger.Minutas.Domain.Enums;

namespace Jaeger.Minutas.UserInterface {
    public partial class MinutaEditarForm : Form {
        private readonly MinutaDetalleService _service;
        private readonly UsuarioCorporativoService _usuariosService;
        private readonly ObtenerMinutaService _minutaService;
        private readonly AdministrarMinutaService _administrarService;
        private List<UsuarioCorporativo> _usuarios = new List<UsuarioCorporativo>();
        private readonly long _minutaId;
        private readonly long _usuarioId;
        private MinutaDetalle _detalle;
        private Minuta _minutaActual;
        private bool _eventosConfigurados;

        public MinutaEditarForm(long minutaId, long usuarioId, MinutaDetalleService service, UsuarioCorporativoService usuariosService, ObtenerMinutaService minutaService, AdministrarMinutaService administrarService, string titulo) {
            if (minutaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(minutaId));
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _usuariosService = usuariosService ?? throw new ArgumentNullException(nameof(usuariosService));
            _minutaService = minutaService ?? throw new ArgumentNullException(nameof(minutaService));
            _administrarService = administrarService ?? throw new ArgumentNullException(nameof(administrarService));
            _minutaId = minutaId;
            _usuarioId = usuarioId;
            Text = "Detalle de minuta - " + titulo;
            InitializeComponent();
            ConfigurarBarrasHerramientas();
            btnGuardar.Click += async (s, e) => await GuardarAsync();
            _editarGeneral.Click += async (s, e) => await EditarGeneralAsync();
            _cambiarEstado.Click += async (s, e) => await CambiarEstadoAsync();
            _estado.Text = "Agregue filas directamente en las tablas.";
            Shown += async (s, e) => await CargarAsync();
        }

        private void ConfigurarBarrasHerramientas() {
            //AgregarBarraHerramientas(tabPage1, _asistentes, "asistentes");
            //AgregarBarraHerramientas(tabPage2, _temas, "temas");
            //AgregarBarraHerramientas(tabPage3, _acuerdos, "acuerdos");
            //AgregarBarraHerramientas(tabPage4, _tareas, "tareas");
        }

        private void AgregarBarraHerramientas(TabPage pagina, DataGridView grid, string tipo) {
            var barra = new ToolStrip {
                Name = "toolStrip" + tipo,
                Dock = DockStyle.Top,
                GripStyle = ToolStripGripStyle.Hidden,
                ImageScalingSize = new System.Drawing.Size(16, 16),
                Padding = new Padding(4, 2, 4, 2)
            };
            var agregar = new ToolStripButton("Agregar") { Name = "btnAgregar" + tipo, ToolTipText = "Agregar un elemento" };
            var remover = new ToolStripButton("Remover") { Name = "btnRemover" + tipo, ToolTipText = "Remover el elemento seleccionado" };
            agregar.Click += (s, e) => AgregarElemento(tipo);
            remover.Click += (s, e) => RemoverElemento(tipo, grid);
            barra.Items.Add(agregar);
            barra.Items.Add(new ToolStripSeparator());
            barra.Items.Add(remover);
            pagina.Controls.Add(barra);
            barra.BringToFront();
            grid.Dock = DockStyle.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
        }

        private void AgregarElemento(string tipo) {
            if (_detalle == null || _minutaActual == null || _minutaActual.Estado == EstadoMinuta.Aprobada || _minutaActual.Estado == EstadoMinuta.Cancelada)
                return;

            switch (tipo) {
                case "asistentes":
                    ((BindingList<MinutaAsistente>)_asistentes.DataSource).Add(new MinutaAsistente {
                        IdMinuta = _minutaId,
                        Rol = string.Empty,
                        EstadoAsistencia = "Convocado"
                    });
                    _asistentes.CurrentCell = _asistentes.Rows[_asistentes.Rows.Count - 1].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
                    break;
                case "temas":
                    ((BindingList<MinutaTema>)_temas.DataSource).Add(new MinutaTema {
                        MinutaId = _minutaId,
                        Orden = ((BindingList<MinutaTema>)_temas.DataSource).Count + 1,
                        Tema = string.Empty,
                        Descripcion = string.Empty,
                        Estado = "Pendiente"
                    });
                    _temas.CurrentCell = _temas.Rows[_temas.Rows.Count - 1].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
                    ActualizarOpcionesRelacion();
                    break;
                case "acuerdos":
                    ((BindingList<MinutaAcuerdo>)_acuerdos.DataSource).Add(new MinutaAcuerdo {
                        MinutaId = _minutaId,
                        Orden = ((BindingList<MinutaAcuerdo>)_acuerdos.DataSource).Count + 1,
                        Tipo = "Acuerdo",
                        Descripcion = string.Empty,
                        RegistradoPor = _usuarioId,
                        FechaRegistro = DateTime.Now
                    });
                    _acuerdos.CurrentCell = _acuerdos.Rows[_acuerdos.Rows.Count - 1].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
                    ActualizarOpcionesRelacion();
                    break;
                case "tareas":
                    ((BindingList<MinutaTarea>)_tareas.DataSource).Add(new MinutaTarea {
                        MinutaId = _minutaId,
                        Descripcion = string.Empty,
                        FechaLimite = DateTime.Today,
                        Estado = "Pendiente",
                        CreadoPor = _usuarioId
                    });
                    _tareas.CurrentCell = _tareas.Rows[_tareas.Rows.Count - 1].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
                    break;
            }
        }

        private void RemoverElemento(string tipo, DataGridView grid) {
            if (_detalle == null || _minutaActual == null || _minutaActual.Estado == EstadoMinuta.Aprobada || _minutaActual.Estado == EstadoMinuta.Cancelada)
                return;
            if (grid.CurrentRow == null || grid.CurrentRow.IsNewRow || grid.CurrentRow.DataBoundItem == null) {
                MessageBox.Show("Seleccione el elemento que desea remover.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("¿Desea remover el elemento seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            switch (tipo) {
                case "asistentes":
                    ((BindingList<MinutaAsistente>)grid.DataSource).Remove((MinutaAsistente)grid.CurrentRow.DataBoundItem);
                    break;
                case "temas":
                    ((BindingList<MinutaTema>)grid.DataSource).Remove((MinutaTema)grid.CurrentRow.DataBoundItem);
                    RenumerarTemas();
                    ActualizarOpcionesRelacion();
                    break;
                case "acuerdos":
                    ((BindingList<MinutaAcuerdo>)grid.DataSource).Remove((MinutaAcuerdo)grid.CurrentRow.DataBoundItem);
                    RenumerarAcuerdos();
                    ActualizarOpcionesRelacion();
                    break;
                case "tareas":
                    ((BindingList<MinutaTarea>)grid.DataSource).Remove((MinutaTarea)grid.CurrentRow.DataBoundItem);
                    break;
            }
        }

        private void RenumerarTemas() {
            var temas = (BindingList<MinutaTema>)_temas.DataSource;
            for (int i = 0; i < temas.Count; i++) temas[i].Orden = i + 1;
            _temas.Refresh();
        }

        private void RenumerarAcuerdos() {
            var acuerdos = (BindingList<MinutaAcuerdo>)_acuerdos.DataSource;
            for (int i = 0; i < acuerdos.Count; i++) acuerdos[i].Orden = i + 1;
            _acuerdos.Refresh();
        }

        private static DataGridView CrearGrid() {
            var grid = new DataGridView {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToOrderColumns = false
            };
            grid.DataError += (s, e) => { e.ThrowException = false; };
            return grid;
        }

        private async Task CargarAsync() {
            try {
                _estado.Text = "Cargando...";
                var minuta = await _minutaService.EjecutarAsync(_minutaId);
                if (minuta == null)
                    throw new InvalidOperationException("La minuta no existe.");
                _minutaActual = minuta;
                _folio.Text = minuta.Folio;
                _titulo.Text = minuta.Titulo;
                cmbTipo.Text = minuta.Tipo;
                dtpFechaReunion.Value = minuta.FechaReunion;
                _horario.Text = minuta.HoraInicio.ToString(@"hh\:mm") + (minuta.HoraFin.HasValue ? " - " + minuta.HoraFin.Value.ToString(@"hh\:mm") : "");
                _lugar.Text = string.Join(" / ", new[] { minuta.Lugar, minuta.Plataforma }.Where(x => !string.IsNullOrWhiteSpace(x)));
                _estadoMinuta.Text = minuta.Estado.ToString();
                _objetivo.Text = minuta.Objetivo;
                _usuarios = await _usuariosService.ObtenerActivosAsync();
                if (_usuarios.Count == 0)
                    _estado.Text = "No hay usuarios activos; no podrán asignarse responsables hasta actualizar el catálogo.";
                _detalle = await _service.ObtenerAsync(_minutaId);
                if (_detalle == null)
                    _detalle = new MinutaDetalle { MinutaId = _minutaId };
                _asistentes.DataSource = new BindingList<MinutaAsistente>(_detalle.Asistentes);
                _temas.DataSource = new BindingList<MinutaTema>(_detalle.Temas);
                _acuerdos.DataSource = new BindingList<MinutaAcuerdo>(_detalle.Acuerdos);
                _tareas.DataSource = new BindingList<MinutaTarea>(_detalle.Tareas);
                ConfigurarColumnas();
                if (!_eventosConfigurados) {
                    _temas.CellValueChanged += (s, e) => ActualizarOpcionesRelacion();
                    _temas.RowsAdded += (s, e) => ActualizarOpcionesRelacion();
                    _acuerdos.CellValueChanged += (s, e) => ActualizarOpcionesRelacion();
                    _acuerdos.RowsAdded += (s, e) => ActualizarOpcionesRelacion();
                    _asistentes.CellValueChanged += AsistenteSeleccionado;
                    _asistentes.CurrentCellDirtyStateChanged += (s, e) => {
                        if (_asistentes.IsCurrentCellDirty)
                            _asistentes.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    };
                    _eventosConfigurados = true;
                }
                ActualizarOpcionesRelacion();
                AplicarPermisosPorEstado(minuta);
                _estado.Text = "Minuta ID: " + _minutaId + " | Versión: " + minuta.VersionFila;
            } catch (Exception) {
                _estado.Text = "No fue posible cargar el detalle.";
                MessageBox.Show("No fue posible cargar el detalle de la minuta.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarPermisosPorEstado(Minuta minuta) {
            bool editable = minuta.Estado != EstadoMinuta.Aprobada && minuta.Estado != EstadoMinuta.Cancelada;
            btnGuardar.Enabled = editable;
            _editarGeneral.Enabled = editable;
            _cambiarEstado.Enabled = editable;
            foreach (DataGridView grid in new[] { _asistentes, _temas, _acuerdos, _tareas }) {
                grid.ReadOnly = !editable;
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
            }
            foreach (TabPage pagina in tabControl1.TabPages) {
                foreach (Control control in pagina.Controls) {
                    var barra = control as ToolStrip;
                    if (barra != null) barra.Enabled = editable;
                }
            }
            if (!editable)
                _estado.Text = "Minuta " + minuta.Estado + ": el detalle está bloqueado.";
        }

        private async Task EditarGeneralAsync() {
            if (_minutaActual == null)
                return;
            try {
                //using (var form = new MinutaEditarForm(_minutaActual, _administrarService)) {
                //    if (form.ShowDialog(this) == DialogResult.OK)
                //        await CargarAsync();
                //}
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Edición de minuta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task CambiarEstadoAsync() {
            if (_minutaActual == null)
                return;
            var opciones = new List<EstadoMinuta>();
            switch (_minutaActual.Estado) {
                case EstadoMinuta.Borrador:
                    opciones.Add(EstadoMinuta.Preparada);
                    opciones.Add(EstadoMinuta.Cancelada);
                    break;
                case EstadoMinuta.Preparada:
                    opciones.Add(EstadoMinuta.EnReunion);
                    opciones.Add(EstadoMinuta.Cancelada);
                    break;
                case EstadoMinuta.EnReunion:
                    opciones.Add(EstadoMinuta.PendienteAprobacion);
                    opciones.Add(EstadoMinuta.Cancelada);
                    break;
                case EstadoMinuta.PendienteAprobacion:
                    opciones.Add(EstadoMinuta.Aprobada);
                    opciones.Add(EstadoMinuta.EnReunion);
                    opciones.Add(EstadoMinuta.Cancelada);
                    break;
                default:
                    return;
            }
            using (var selector = new Form { Text = "Cambiar estado", StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MinimizeBox = false, MaximizeBox = false, Width = 360, Height = 150 }) {
                var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Left = 12, Top = 12, Width = 320 };
                foreach (var opcion in opciones)
                    combo.Items.Add(opcion);
                if (combo.Items.Count > 0)
                    combo.SelectedIndex = 0;
                var aceptar = new Button { Text = "Aplicar", DialogResult = DialogResult.OK, Left = 172, Top = 55, Width = 75 };
                var cancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Left = 257, Top = 55, Width = 75 };
                selector.Controls.Add(combo);
                selector.Controls.Add(aceptar);
                selector.Controls.Add(cancelar);
                selector.AcceptButton = aceptar;
                selector.CancelButton = cancelar;
                if (selector.ShowDialog(this) != DialogResult.OK || combo.SelectedItem == null)
                    return;
                try {
                    _cambiarEstado.Enabled = false;
                    await _administrarService.CambiarEstadoAsync(_minutaId, _minutaActual.VersionFila, (EstadoMinuta)combo.SelectedItem);
                    await CargarAsync();
                    _estado.Text = "Estado actualizado correctamente.";
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "No se pudo cambiar el estado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } finally {
                    if (_minutaActual != null)
                        AplicarPermisosPorEstado(_minutaActual);
                    else
                        _cambiarEstado.Enabled = true;
                }
            }
        }

        private void ConfigurarColumnas() {
            Ocultar(_asistentes, "Id", "IdMinuta");
            ConfigurarColumnaUsuario(_asistentes, "IdUsuario", "Usuario corporativo", true);
            if (_asistentes.Columns.Contains("Nombre"))
                _asistentes.Columns["Nombre"].ReadOnly = true;
            if (_asistentes.Columns.Contains("Correo"))
                _asistentes.Columns["Correo"].ReadOnly = true;
            Ocultar(_temas, "Id", "MinutaId");
            ConfigurarColumnaUsuario(_temas, "ResponsableId", "Responsable", true);
            Ocultar(_acuerdos, "Id", "MinutaId", "TemaId", "RegistradoPor", "FechaRegistro");
            ConfigurarColumnaRelacion(_acuerdos, "TemaOrdenSeleccionado", "Tema relacionado");
            Ocultar(_tareas, "Id", "MinutaId", "AcuerdoId", "CreadoPor", "FechaFinalizacion");
            ConfigurarColumnaRelacion(_tareas, "AcuerdoOrdenSeleccionado", "Acuerdo relacionado");
            ConfigurarColumnaUsuario(_tareas, "ResponsableId", "Responsable", false);
            AjustarFecha(_tareas, "FechaLimite");
            AjustarFecha(_tareas, "FechaFinalizacion");
            _tareas.DataError += (s, e) => { e.ThrowException = false; };
        }

        private static void Ocultar(DataGridView grid, params string[] names) {
            foreach (string name in names)
                if (grid.Columns.Contains(name))
                    grid.Columns[name].Visible = false;
        }

        private static void AjustarFecha(DataGridView grid, string name) {
            if (grid.Columns.Contains(name))
                grid.Columns[name].DefaultCellStyle.Format = "yyyy-MM-dd";
        }

        private void ConfigurarColumnaRelacion(DataGridView grid, string propertyName, string header) {
            if (!grid.Columns.Contains(propertyName))
                return;
            int index = grid.Columns[propertyName].Index;
            grid.Columns.Remove(propertyName);
            grid.Columns.Insert(index, new DataGridViewComboBoxColumn {
                Name = propertyName,
                DataPropertyName = propertyName,
                HeaderText = header,
                DisplayMember = "Texto",
                ValueMember = "Orden",
                ValueType = typeof(int),
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                DisplayStyleForCurrentCellOnly = true,
                FlatStyle = FlatStyle.Flat,
                Width = 230
            });
        }

        private void ActualizarOpcionesRelacion() {
            if (_detalle == null || _acuerdos == null || _tareas == null)
                return;
            var temas = ((BindingList<MinutaTema>)_temas.DataSource).Select((x, i) => new RelacionOpcion { Orden = i + 1, Texto = (i + 1) + ". " + (x.Tema ?? string.Empty) }).ToList();
            var acuerdos = ((BindingList<MinutaAcuerdo>)_acuerdos.DataSource).Select((x, i) => new RelacionOpcion { Orden = i + 1, Texto = (i + 1) + ". " + (x.Descripcion ?? string.Empty) }).ToList();
            if (_acuerdos.Columns.Contains("TemaOrdenSeleccionado")) {
                var col = (DataGridViewComboBoxColumn)_acuerdos.Columns["TemaOrdenSeleccionado"];
                col.DataSource = temas.ToList();
            }
            if (_tareas.Columns.Contains("AcuerdoOrdenSeleccionado")) {
                var col = (DataGridViewComboBoxColumn)_tareas.Columns["AcuerdoOrdenSeleccionado"];
                col.DataSource = acuerdos.ToList();
            }
        }

        private sealed class RelacionOpcion {
            public int Orden { get; set; }
            public string Texto { get; set; }
        }

        private void ConfigurarColumnaUsuario(DataGridView grid, string propertyName, string header, bool permitirNulo) {
            if (!grid.Columns.Contains(propertyName))
                return;
            int index = grid.Columns[propertyName].Index;
            grid.Columns.Remove(propertyName);
            var columna = new DataGridViewComboBoxColumn {
                Name = propertyName,
                DataPropertyName = propertyName,
                HeaderText = header,
                DataSource = _usuarios,
                DisplayMember = "NombreMostrar",
                ValueMember = "Id",
                ValueType = typeof(long),
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                DisplayStyleForCurrentCellOnly = true,
                FlatStyle = FlatStyle.Flat,
                SortMode = DataGridViewColumnSortMode.Automatic,
                Width = 220
            };
            columna.DefaultCellStyle.NullValue = null;
            if (!permitirNulo)
                columna.DefaultCellStyle.NullValue = null;
            grid.Columns.Insert(index, columna);
        }

        private void AsistenteSeleccionado(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || _asistentes.Columns[e.ColumnIndex].Name != "IdUsuario")
                return;
            var asistente = _asistentes.Rows[e.RowIndex].DataBoundItem as MinutaAsistente;
            if (asistente == null || !asistente.IdUsuario.HasValue)
                return;
            UsuarioCorporativo usuario = _usuarios.Find(x => x.Id == asistente.IdUsuario.Value);
            if (usuario == null)
                return;
            asistente.Nombre = usuario.Nombre;
            asistente.Correo = usuario.Correo;
            _asistentes.Refresh();
        }

        private async Task GuardarAsync() {
            btnGuardar.Enabled = false;
            try {
                ValidateChildren();
                _asistentes.EndEdit();
                _temas.EndEdit();
                _acuerdos.EndEdit();
                _tareas.EndEdit();
                _detalle.Asistentes = ((BindingList<MinutaAsistente>)_asistentes.DataSource).ToListSafe();
                _detalle.Temas = ((BindingList<MinutaTema>)_temas.DataSource).ToListSafe();
                _detalle.Acuerdos = ((BindingList<MinutaAcuerdo>)_acuerdos.DataSource).ToListSafe();
                _detalle.Tareas = ((BindingList<MinutaTarea>)_tareas.DataSource).ToListSafe();
                await _service.GuardarAsync(_detalle, _usuarioId);
                _minutaActual = await _minutaService.EjecutarAsync(_minutaId);
                _estadoMinuta.Text = _minutaActual.Estado.ToString();
                AplicarPermisosPorEstado(_minutaActual);
                _estado.Text = "Detalle guardado correctamente. Versión: " + _minutaActual.VersionFila;
                MessageBox.Show("Se guardaron asistentes, temas, acuerdos y tareas.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (InvalidOperationException ex) {
                _estado.Text = "No se guardaron los cambios.";
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } catch (Exception) {
                _estado.Text = "Error al guardar.";
                MessageBox.Show("No fue posible guardar el detalle. Verifique el registro de errores y la base de datos.", "Minutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                if (_minutaActual != null)
                    AplicarPermisosPorEstado(_minutaActual);
                else
                    btnGuardar.Enabled = true;
            }
        }
    }
}
