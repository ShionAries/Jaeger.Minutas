namespace Jaeger.Minutas.UserInterface {
    partial class MinutaNuevoForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinutaNuevoForm));
            this.lblFolio = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFechaReunion = new System.Windows.Forms.DateTimePicker();
            this.lblHora = new System.Windows.Forms.Label();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.lblPlataforma = new System.Windows.Forms.Label();
            this.txtPlataforma = new System.Windows.Forms.TextBox();
            this.lblObjetivo = new System.Windows.Forms.Label();
            this.txtObjetivo = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.tabMinutas = new System.Windows.Forms.TabControl();
            this.tabAsistentes = new System.Windows.Forms.TabPage();
            this._asistentes = new System.Windows.Forms.DataGridView();
            this.TAsistentes = new System.Windows.Forms.ToolStrip();
            this.TAsistenteAgregar = new System.Windows.Forms.ToolStripButton();
            this.TAsistenteRemover = new System.Windows.Forms.ToolStripButton();
            this.grpMinuta = new System.Windows.Forms.GroupBox();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinutaId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdUsuario = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Correo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabMinutas.SuspendLayout();
            this.tabAsistentes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._asistentes)).BeginInit();
            this.TAsistentes.SuspendLayout();
            this.grpMinuta.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFolio
            // 
            this.lblFolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFolio.AutoSize = true;
            this.lblFolio.Location = new System.Drawing.Point(282, 23);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(29, 13);
            this.lblFolio.TabIndex = 0;
            this.lblFolio.Text = "Folio";
            // 
            // txtFolio
            // 
            this.txtFolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolio.Location = new System.Drawing.Point(315, 19);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.Size = new System.Drawing.Size(102, 20);
            this.txtFolio.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(13, 23);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(35, 13);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Título";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitulo.Location = new System.Drawing.Point(73, 19);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(200, 20);
            this.txtTitulo.TabIndex = 1;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(13, 49);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(28, 13);
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "Tipo";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(73, 45);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(194, 21);
            this.cmbTipo.TabIndex = 2;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(13, 76);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Fecha";
            // 
            // dtpFechaReunion
            // 
            this.dtpFechaReunion.Location = new System.Drawing.Point(73, 72);
            this.dtpFechaReunion.Name = "dtpFechaReunion";
            this.dtpFechaReunion.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaReunion.TabIndex = 3;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(279, 76);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(30, 13);
            this.lblHora.TabIndex = 0;
            this.lblHora.Text = "Hora";
            // 
            // dtpHora
            // 
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHora.Location = new System.Drawing.Point(315, 71);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.Size = new System.Drawing.Size(102, 20);
            this.dtpHora.TabIndex = 3;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Location = new System.Drawing.Point(13, 102);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(34, 13);
            this.lblUbicacion.TabIndex = 0;
            this.lblUbicacion.Text = "Lugar";
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUbicacion.Location = new System.Drawing.Point(73, 98);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(344, 20);
            this.txtUbicacion.TabIndex = 1;
            // 
            // lblPlataforma
            // 
            this.lblPlataforma.AutoSize = true;
            this.lblPlataforma.Location = new System.Drawing.Point(13, 128);
            this.lblPlataforma.Name = "lblPlataforma";
            this.lblPlataforma.Size = new System.Drawing.Size(57, 13);
            this.lblPlataforma.TabIndex = 0;
            this.lblPlataforma.Text = "Plataforma";
            // 
            // txtPlataforma
            // 
            this.txtPlataforma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlataforma.Location = new System.Drawing.Point(73, 124);
            this.txtPlataforma.Name = "txtPlataforma";
            this.txtPlataforma.Size = new System.Drawing.Size(344, 20);
            this.txtPlataforma.TabIndex = 1;
            // 
            // lblObjetivo
            // 
            this.lblObjetivo.AutoSize = true;
            this.lblObjetivo.Location = new System.Drawing.Point(13, 152);
            this.lblObjetivo.Name = "lblObjetivo";
            this.lblObjetivo.Size = new System.Drawing.Size(46, 13);
            this.lblObjetivo.TabIndex = 0;
            this.lblObjetivo.Text = "Objetivo";
            // 
            // txtObjetivo
            // 
            this.txtObjetivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjetivo.Location = new System.Drawing.Point(73, 150);
            this.txtObjetivo.Multiline = true;
            this.txtObjetivo.Name = "txtObjetivo";
            this.txtObjetivo.Size = new System.Drawing.Size(344, 60);
            this.txtObjetivo.TabIndex = 1;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Location = new System.Drawing.Point(369, 476);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cancelar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(288, 476);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(273, 49);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Estado";
            // 
            // cboStatus
            // 
            this.cboStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(315, 45);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(102, 21);
            this.cboStatus.TabIndex = 2;
            // 
            // tabMinutas
            // 
            this.tabMinutas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMinutas.Controls.Add(this.tabAsistentes);
            this.tabMinutas.Location = new System.Drawing.Point(12, 236);
            this.tabMinutas.Name = "tabMinutas";
            this.tabMinutas.SelectedIndex = 0;
            this.tabMinutas.Size = new System.Drawing.Size(432, 227);
            this.tabMinutas.TabIndex = 5;
            // 
            // tabAsistentes
            // 
            this.tabAsistentes.Controls.Add(this._asistentes);
            this.tabAsistentes.Controls.Add(this.TAsistentes);
            this.tabAsistentes.Location = new System.Drawing.Point(4, 22);
            this.tabAsistentes.Name = "tabAsistentes";
            this.tabAsistentes.Padding = new System.Windows.Forms.Padding(3);
            this.tabAsistentes.Size = new System.Drawing.Size(424, 201);
            this.tabAsistentes.TabIndex = 0;
            this.tabAsistentes.Text = "Asistentes";
            this.tabAsistentes.UseVisualStyleBackColor = true;
            // 
            // _asistentes
            // 
            this._asistentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._asistentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.MinutaId,
            this.IdUsuario,
            this.Nombre,
            this.Correo});
            this._asistentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this._asistentes.Location = new System.Drawing.Point(3, 28);
            this._asistentes.Name = "_asistentes";
            this._asistentes.Size = new System.Drawing.Size(418, 170);
            this._asistentes.TabIndex = 1;
            // 
            // TAsistentes
            // 
            this.TAsistentes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TAsistenteAgregar,
            this.TAsistenteRemover});
            this.TAsistentes.Location = new System.Drawing.Point(3, 3);
            this.TAsistentes.Name = "TAsistentes";
            this.TAsistentes.Size = new System.Drawing.Size(418, 25);
            this.TAsistentes.TabIndex = 2;
            this.TAsistentes.Text = "Asistente";
            // 
            // TAsistenteAgregar
            // 
            this.TAsistenteAgregar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TAsistenteAgregar.Image = ((System.Drawing.Image)(resources.GetObject("TAsistenteAgregar.Image")));
            this.TAsistenteAgregar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TAsistenteAgregar.Name = "TAsistenteAgregar";
            this.TAsistenteAgregar.Size = new System.Drawing.Size(53, 22);
            this.TAsistenteAgregar.Text = "Agregar";
            // 
            // TAsistenteRemover
            // 
            this.TAsistenteRemover.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TAsistenteRemover.Image = ((System.Drawing.Image)(resources.GetObject("TAsistenteRemover.Image")));
            this.TAsistenteRemover.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TAsistenteRemover.Name = "TAsistenteRemover";
            this.TAsistenteRemover.Size = new System.Drawing.Size(58, 22);
            this.TAsistenteRemover.Text = "Remover";
            // 
            // grpMinuta
            // 
            this.grpMinuta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMinuta.Controls.Add(this.txtTitulo);
            this.grpMinuta.Controls.Add(this.lblFolio);
            this.grpMinuta.Controls.Add(this.txtFolio);
            this.grpMinuta.Controls.Add(this.lblTitulo);
            this.grpMinuta.Controls.Add(this.dtpHora);
            this.grpMinuta.Controls.Add(this.lblTipo);
            this.grpMinuta.Controls.Add(this.dtpFechaReunion);
            this.grpMinuta.Controls.Add(this.lblUbicacion);
            this.grpMinuta.Controls.Add(this.cboStatus);
            this.grpMinuta.Controls.Add(this.lblStatus);
            this.grpMinuta.Controls.Add(this.cmbTipo);
            this.grpMinuta.Controls.Add(this.lblFecha);
            this.grpMinuta.Controls.Add(this.lblHora);
            this.grpMinuta.Controls.Add(this.lblPlataforma);
            this.grpMinuta.Controls.Add(this.txtObjetivo);
            this.grpMinuta.Controls.Add(this.lblObjetivo);
            this.grpMinuta.Controls.Add(this.txtPlataforma);
            this.grpMinuta.Controls.Add(this.txtUbicacion);
            this.grpMinuta.Location = new System.Drawing.Point(12, 12);
            this.grpMinuta.Name = "grpMinuta";
            this.grpMinuta.Size = new System.Drawing.Size(432, 218);
            this.grpMinuta.TabIndex = 6;
            this.grpMinuta.TabStop = false;
            this.grpMinuta.Text = "Información";
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // MinutaId
            // 
            this.MinutaId.DataPropertyName = "IdMinuta";
            this.MinutaId.HeaderText = "MinutaId";
            this.MinutaId.Name = "MinutaId";
            this.MinutaId.ReadOnly = true;
            // 
            // IdUsuario
            // 
            this.IdUsuario.DataPropertyName = "IdUsuario";
            this.IdUsuario.HeaderText = "UsuarioId";
            this.IdUsuario.Name = "IdUsuario";
            this.IdUsuario.ReadOnly = true;
            this.IdUsuario.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IdUsuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Correo
            // 
            this.Correo.DataPropertyName = "Correo";
            this.Correo.HeaderText = "Correo";
            this.Correo.Name = "Correo";
            this.Correo.ReadOnly = true;
            // 
            // MinutaNuevoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(456, 511);
            this.Controls.Add(this.grpMinuta);
            this.Controls.Add(this.tabMinutas);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCerrar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MinutaNuevoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Minuta Nuevo";
            this.tabMinutas.ResumeLayout(false);
            this.tabAsistentes.ResumeLayout(false);
            this.tabAsistentes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._asistentes)).EndInit();
            this.TAsistentes.ResumeLayout(false);
            this.TAsistentes.PerformLayout();
            this.grpMinuta.ResumeLayout(false);
            this.grpMinuta.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFechaReunion;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Label lblPlataforma;
        private System.Windows.Forms.TextBox txtPlataforma;
        private System.Windows.Forms.Label lblObjetivo;
        private System.Windows.Forms.TextBox txtObjetivo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.TabControl tabMinutas;
        private System.Windows.Forms.TabPage tabAsistentes;
        private System.Windows.Forms.GroupBox grpMinuta;
        private System.Windows.Forms.DataGridView _asistentes;
        private System.Windows.Forms.ToolStrip TAsistentes;
        private System.Windows.Forms.ToolStripButton TAsistenteAgregar;
        private System.Windows.Forms.ToolStripButton TAsistenteRemover;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinutaId;
        private System.Windows.Forms.DataGridViewComboBoxColumn IdUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Correo;
    }
}