using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaeger.Minutas.Aplication.Services;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.UserInterface {
    public sealed class MinutaAuditoria1Form : Form {
        private readonly MinutaAuditoriaService _service;
        private readonly TextBox _folioOId;
        private readonly NumericUpDown _usuarioId;
        private readonly ComboBox _accion;
        private readonly DateTimePicker _desde;
        private readonly DateTimePicker _hasta;
        private readonly Button _buscar;
        private readonly DataGridView _resultados;
        private readonly TextBox _anterior;
        private readonly TextBox _nuevo;
        private readonly Label _estado;
        private readonly BindingSource _binding = new BindingSource();

        public MinutaAuditoria1Form(MinutaAuditoriaService service) {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            Text = "Auditoría de minutas";
            StartPosition = FormStartPosition.CenterParent;
            Width = 1250;
            Height = 820;
            MinimumSize = new Size(920, 620);

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10), ColumnCount = 1, RowCount = 3 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 104));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 56));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 44));

            var filtros = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 7, RowCount = 2, Padding = new Padding(4) };
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105));
            filtros.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            filtros.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            AgregarFiltro(filtros, "Folio o ID de minuta", 0, 0);
            AgregarFiltro(filtros, "ID de usuario (0 = todos)", 1, 0);
            AgregarFiltro(filtros, "Operación", 2, 0);
            AgregarFiltro(filtros, "Fecha desde (opcional)", 4, 0);
            AgregarFiltro(filtros, "Fecha hasta (opcional)", 5, 0);

            _folioOId = new TextBox { Dock = DockStyle.Fill };
            _usuarioId = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 0, Maximum = int.MaxValue, Value = 0, ThousandsSeparator = true };
            _accion = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            _accion.Items.AddRange(new object[] { "Todas", "CREAR", "ACTUALIZAR_DATOS", "CAMBIAR_ESTADO", "ELIMINAR_LOGICO", "ACTUALIZAR_DETALLE" });
            _accion.SelectedIndex = 0;
            _desde = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false };
            _hasta = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false };
            _buscar = new Button { Text = "Buscar", Dock = DockStyle.Fill, MinimumSize = new Size(85, 30) };
            _buscar.Click += async (s, e) => await BuscarAsync();
            filtros.Controls.Add(_folioOId, 0, 1);
            filtros.Controls.Add(_usuarioId, 1, 1);
            filtros.Controls.Add(_accion, 2, 1);
            filtros.Controls.Add(_desde, 4, 1);
            filtros.Controls.Add(_hasta, 5, 1);
            filtros.Controls.Add(_buscar, 6, 1);
            _estado = new Label { Dock = DockStyle.Fill, AutoSize = false, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Color.DimGray };
            filtros.Controls.Add(_estado, 3, 1);
            root.Controls.Add(filtros, 0, 0);

            _resultados = new DataGridView {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _resultados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha y hora", DataPropertyName = "FechaEvento", FillWeight = 23, DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" } });
            _resultados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Folio", DataPropertyName = "Folio", FillWeight = 13 });
            _resultados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Minuta ID", DataPropertyName = "MinutaId", FillWeight = 10 });
            _resultados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuario", DataPropertyName = "Usuario", FillWeight = 23 });
            _resultados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuario ID", DataPropertyName = "UsuarioId", FillWeight = 10 });
            _resultados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Operación", DataPropertyName = "Accion", FillWeight = 21 });
            _resultados.DataSource = _binding;
            _resultados.SelectionChanged += (s, e) => MostrarSeleccion();
            root.Controls.Add(_resultados, 0, 1);

            var comparacion = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical };
            _anterior = CrearTextoComparacion();
            _nuevo = CrearTextoComparacion();
            comparacion.Panel1.Controls.Add(CrearPanelComparacion("Datos anteriores", _anterior));
            comparacion.Panel2.Controls.Add(CrearPanelComparacion("Datos nuevos", _nuevo));
            root.Controls.Add(comparacion, 0, 2);
            Controls.Add(root);
        }

        private static void AgregarFiltro(TableLayoutPanel panel, string texto, int columna, int fila) {
            panel.Controls.Add(new Label { Text = texto, Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft, AutoSize = false }, columna, fila);
        }

        private static TextBox CrearTextoComparacion() {
            return new TextBox {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                WordWrap = false,
                Font = new Font("Consolas", 9F),
                BackColor = SystemColors.Window
            };
        }

        private static Control CrearPanelComparacion(string titulo, TextBox texto) {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(4) };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.Controls.Add(new Label { Text = titulo, Dock = DockStyle.Fill, Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
            panel.Controls.Add(texto, 0, 1);
            return panel;
        }

        private async Task BuscarAsync() {
            DateTime? desde = _desde.Checked ? (DateTime?)_desde.Value.Date : null;
            DateTime? hasta = _hasta.Checked ? (DateTime?)_hasta.Value.Date : null;
            if (desde.HasValue && hasta.HasValue && desde.Value > hasta.Value) {
                MessageBox.Show(this, "La fecha inicial no puede ser posterior a la fecha final.", "Filtros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _buscar.Enabled = false;
            _estado.Text = "Consultando...";
            try {
                var filtro = new MinutaAuditoriaFiltro {
                    FolioOIdMinuta = _folioOId.Text,
                    UsuarioId = _usuarioId.Value > 0 ? (int?)Decimal.ToInt32(_usuarioId.Value) : null,
                    Accion = _accion.SelectedIndex > 0 ? _accion.SelectedItem.ToString() : null,
                    Desde = desde,
                    Hasta = hasta,
                    Limite = 500
                };
                List<MinutaAuditoriaRegistro> registros = await _service.BuscarAsync(filtro);
                _binding.DataSource = registros;
                _estado.Text = registros.Count == 500 ? "Se muestran 500 registros como máximo; refine los filtros si necesita más precisión." : "Registros encontrados: " + registros.Count;
                if (registros.Count == 0) { _anterior.Clear(); _nuevo.Clear(); } else if (_resultados.Rows.Count > 0)
                    _resultados.Rows[0].Selected = true;
                MostrarSeleccion();
            } catch (ArgumentException ex) {
                MessageBox.Show(this, ex.Message, "Filtros inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _estado.Text = "Revise los filtros.";
            } catch (Exception) {
                MessageBox.Show(this, "No fue posible consultar la auditoría. Verifique la conexión y que la tabla MINUTA_AUDITORIA exista.", "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _estado.Text = "La consulta falló.";
            } finally {
                _buscar.Enabled = true;
            }
        }

        private void MostrarSeleccion() {
            var registro = _resultados.CurrentRow == null ? null : _resultados.CurrentRow.DataBoundItem as MinutaAuditoriaRegistro;
            _anterior.Text = registro == null ? string.Empty : registro.DatosAnteriores ?? string.Empty;
            _nuevo.Text = registro == null ? string.Empty : registro.DatosNuevos ?? string.Empty;
        }
    }
}
