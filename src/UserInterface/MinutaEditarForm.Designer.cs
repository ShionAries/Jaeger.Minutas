namespace Jaeger.Minutas.UserInterface {
    partial class MinutaEditarForm {
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
            this._folio = new System.Windows.Forms.TextBox();
            this.lblFolio = new System.Windows.Forms.Label();
            this._titulo = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this._horario = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaReunion = new System.Windows.Forms.DateTimePicker();
            this._estadoMinuta = new System.Windows.Forms.ComboBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._objetivo = new System.Windows.Forms.TextBox();
            this.txtPlataforma = new System.Windows.Forms.TextBox();
            this._lugar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._asistentes = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._temas = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._acuerdos = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this._tareas = new System.Windows.Forms.DataGridView();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this._estado = new System.Windows.Forms.Label();
            this._cambiarEstado = new System.Windows.Forms.Button();
            this._editarGeneral = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._asistentes)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._temas)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._acuerdos)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tareas)).BeginInit();
            this.grpGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // _folio
            // 
            this._folio.Location = new System.Drawing.Point(43, 19);
            this._folio.Name = "_folio";
            this._folio.Size = new System.Drawing.Size(102, 20);
            this._folio.TabIndex = 3;
            // 
            // lblFolio
            // 
            this.lblFolio.AutoSize = true;
            this.lblFolio.Location = new System.Drawing.Point(11, 23);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(29, 13);
            this.lblFolio.TabIndex = 2;
            this.lblFolio.Text = "Folio";
            // 
            // _titulo
            // 
            this._titulo.Location = new System.Drawing.Point(192, 19);
            this._titulo.Name = "_titulo";
            this._titulo.Size = new System.Drawing.Size(305, 20);
            this._titulo.TabIndex = 5;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(151, 23);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(35, 13);
            this.lblTitulo.TabIndex = 4;
            this.lblTitulo.Text = "Título";
            // 
            // _horario
            // 
            this._horario.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this._horario.Location = new System.Drawing.Point(545, 45);
            this._horario.Name = "_horario";
            this._horario.ShowUpDown = true;
            this._horario.Size = new System.Drawing.Size(102, 20);
            this._horario.TabIndex = 12;
            // 
            // dtpFechaReunion
            // 
            this.dtpFechaReunion.Location = new System.Drawing.Point(286, 45);
            this.dtpFechaReunion.Name = "dtpFechaReunion";
            this.dtpFechaReunion.Size = new System.Drawing.Size(217, 20);
            this.dtpFechaReunion.TabIndex = 13;
            // 
            // _estadoMinuta
            // 
            this._estadoMinuta.FormattingEnabled = true;
            this._estadoMinuta.Location = new System.Drawing.Point(545, 19);
            this._estadoMinuta.Name = "_estadoMinuta";
            this._estadoMinuta.Size = new System.Drawing.Size(102, 21);
            this._estadoMinuta.TabIndex = 10;
            // 
            // cmbTipo
            // 
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(43, 45);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(194, 21);
            this.cmbTipo.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(509, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hora";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Fecha";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(503, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Estado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tipo";
            // 
            // _objetivo
            // 
            this._objetivo.Location = new System.Drawing.Point(63, 98);
            this._objetivo.Multiline = true;
            this._objetivo.Name = "_objetivo";
            this._objetivo.Size = new System.Drawing.Size(584, 40);
            this._objetivo.TabIndex = 17;
            // 
            // txtPlataforma
            // 
            this.txtPlataforma.Location = new System.Drawing.Point(390, 72);
            this.txtPlataforma.Name = "txtPlataforma";
            this.txtPlataforma.Size = new System.Drawing.Size(257, 20);
            this.txtPlataforma.TabIndex = 18;
            // 
            // _lugar
            // 
            this._lugar.Location = new System.Drawing.Point(63, 72);
            this._lugar.Name = "_lugar";
            this._lugar.Size = new System.Drawing.Size(261, 20);
            this._lugar.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Objetivo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Plataforma";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Lugar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(511, 444);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 20;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Location = new System.Drawing.Point(592, 444);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 21;
            this.btnCerrar.Text = "Cancelar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 171);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(658, 263);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._asistentes);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(650, 237);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Asistentes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _asistentes
            // 
            this._asistentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._asistentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this._asistentes.Location = new System.Drawing.Point(3, 28);
            this._asistentes.Name = "_asistentes";
            this._asistentes.Size = new System.Drawing.Size(644, 206);
            this._asistentes.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(644, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._temas);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(650, 237);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Temas de agenda";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _temas
            // 
            this._temas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._temas.Dock = System.Windows.Forms.DockStyle.Fill;
            this._temas.Location = new System.Drawing.Point(3, 3);
            this._temas.Name = "_temas";
            this._temas.Size = new System.Drawing.Size(644, 231);
            this._temas.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._acuerdos);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(650, 237);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Acuerdos y descisiones";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _acuerdos
            // 
            this._acuerdos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._acuerdos.Dock = System.Windows.Forms.DockStyle.Fill;
            this._acuerdos.Location = new System.Drawing.Point(0, 0);
            this._acuerdos.Name = "_acuerdos";
            this._acuerdos.Size = new System.Drawing.Size(650, 237);
            this._acuerdos.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this._tareas);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(650, 237);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tareas y pendientes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // _tareas
            // 
            this._tareas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._tareas.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tareas.Location = new System.Drawing.Point(0, 0);
            this._tareas.Name = "_tareas";
            this._tareas.Size = new System.Drawing.Size(650, 237);
            this._tareas.TabIndex = 1;
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this._folio);
            this.grpGeneral.Controls.Add(this.lblFolio);
            this.grpGeneral.Controls.Add(this.lblTitulo);
            this.grpGeneral.Controls.Add(this._titulo);
            this.grpGeneral.Controls.Add(this._objetivo);
            this.grpGeneral.Controls.Add(this.label1);
            this.grpGeneral.Controls.Add(this.txtPlataforma);
            this.grpGeneral.Controls.Add(this.label7);
            this.grpGeneral.Controls.Add(this._lugar);
            this.grpGeneral.Controls.Add(this.label2);
            this.grpGeneral.Controls.Add(this.label6);
            this.grpGeneral.Controls.Add(this.label3);
            this.grpGeneral.Controls.Add(this.label5);
            this.grpGeneral.Controls.Add(this.cmbTipo);
            this.grpGeneral.Controls.Add(this.label4);
            this.grpGeneral.Controls.Add(this._estadoMinuta);
            this.grpGeneral.Controls.Add(this._horario);
            this.grpGeneral.Controls.Add(this.dtpFechaReunion);
            this.grpGeneral.Location = new System.Drawing.Point(12, 12);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(658, 153);
            this.grpGeneral.TabIndex = 23;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "Minuta";
            // 
            // _estado
            // 
            this._estado.AutoSize = true;
            this._estado.Location = new System.Drawing.Point(13, 449);
            this._estado.Name = "_estado";
            this._estado.Size = new System.Drawing.Size(46, 13);
            this._estado.TabIndex = 14;
            this._estado.Text = "Objetivo";
            // 
            // _cambiarEstado
            // 
            this._cambiarEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cambiarEstado.Location = new System.Drawing.Point(408, 444);
            this._cambiarEstado.Name = "_cambiarEstado";
            this._cambiarEstado.Size = new System.Drawing.Size(97, 23);
            this._cambiarEstado.TabIndex = 20;
            this._cambiarEstado.Text = "Cambiar estado";
            this._cambiarEstado.UseVisualStyleBackColor = true;
            // 
            // _editarGeneral
            // 
            this._editarGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._editarGeneral.Location = new System.Drawing.Point(271, 444);
            this._editarGeneral.Name = "_editarGeneral";
            this._editarGeneral.Size = new System.Drawing.Size(131, 23);
            this._editarGeneral.TabIndex = 20;
            this._editarGeneral.Text = "Editar información";
            this._editarGeneral.UseVisualStyleBackColor = true;
            // 
            // MinutaEditarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 479);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._editarGeneral);
            this.Controls.Add(this._cambiarEstado);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this._estado);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MinutaEditarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._asistentes)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._temas)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._acuerdos)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._tareas)).EndInit();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _folio;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.TextBox _titulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DateTimePicker _horario;
        private System.Windows.Forms.DateTimePicker dtpFechaReunion;
        private System.Windows.Forms.ComboBox _estadoMinuta;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _objetivo;
        private System.Windows.Forms.TextBox txtPlataforma;
        private System.Windows.Forms.TextBox _lugar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.Label _estado;
        private System.Windows.Forms.DataGridView _asistentes;
        private System.Windows.Forms.DataGridView _temas;
        private System.Windows.Forms.DataGridView _acuerdos;
        private System.Windows.Forms.DataGridView _tareas;
        private System.Windows.Forms.Button _cambiarEstado;
        private System.Windows.Forms.Button _editarGeneral;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdAsistente;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdMinuta;
        private System.Windows.Forms.DataGridViewComboBoxColumn IdUsuario;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}