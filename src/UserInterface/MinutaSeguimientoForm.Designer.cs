namespace Jaeger.Minutas.UserInterface {
    partial class MinutaSeguimientoForm {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this._titulo = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this._objetivo = new System.Windows.Forms.TextBox();
            this.lblObjetivo = new System.Windows.Forms.Label();
            this._hora = new System.Windows.Forms.DateTimePicker();
            this._fecha = new System.Windows.Forms.DateTimePicker();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this._lugar = new System.Windows.Forms.TextBox();
            this.lblPlataforma = new System.Windows.Forms.Label();
            this._plataforma = new System.Windows.Forms.TextBox();
            this._grid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this._cancelar = new System.Windows.Forms.Button();
            this._crear = new System.Windows.Forms.Button();
            this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Acuerdo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaLimite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResponsableId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _titulo
            // 
            this._titulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._titulo.Location = new System.Drawing.Point(12, 31);
            this._titulo.Name = "_titulo";
            this._titulo.Size = new System.Drawing.Size(620, 20);
            this._titulo.TabIndex = 3;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(15, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(35, 13);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "Título";
            // 
            // _objetivo
            // 
            this._objetivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._objetivo.Location = new System.Drawing.Point(12, 70);
            this._objetivo.Multiline = true;
            this._objetivo.Name = "_objetivo";
            this._objetivo.Size = new System.Drawing.Size(620, 47);
            this._objetivo.TabIndex = 5;
            // 
            // lblObjetivo
            // 
            this.lblObjetivo.AutoSize = true;
            this.lblObjetivo.Location = new System.Drawing.Point(15, 54);
            this.lblObjetivo.Name = "lblObjetivo";
            this.lblObjetivo.Size = new System.Drawing.Size(46, 13);
            this.lblObjetivo.TabIndex = 4;
            this.lblObjetivo.Text = "Objetivo";
            // 
            // _hora
            // 
            this._hora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this._hora.Location = new System.Drawing.Point(292, 123);
            this._hora.Name = "_hora";
            this._hora.Size = new System.Drawing.Size(102, 20);
            this._hora.TabIndex = 10;
            // 
            // _fecha
            // 
            this._fecha.Location = new System.Drawing.Point(87, 123);
            this._fecha.Name = "_fecha";
            this._fecha.Size = new System.Drawing.Size(200, 20);
            this._fecha.TabIndex = 11;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Location = new System.Drawing.Point(14, 153);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(34, 13);
            this.lblUbicacion.TabIndex = 6;
            this.lblUbicacion.Text = "Lugar";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(14, 127);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(69, 13);
            this.lblFecha.TabIndex = 7;
            this.lblFecha.Text = "Fecha y hora";
            // 
            // _lugar
            // 
            this._lugar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lugar.Location = new System.Drawing.Point(54, 149);
            this._lugar.Name = "_lugar";
            this._lugar.Size = new System.Drawing.Size(233, 20);
            this._lugar.TabIndex = 9;
            // 
            // lblPlataforma
            // 
            this.lblPlataforma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlataforma.AutoSize = true;
            this.lblPlataforma.Location = new System.Drawing.Point(293, 153);
            this.lblPlataforma.Name = "lblPlataforma";
            this.lblPlataforma.Size = new System.Drawing.Size(57, 13);
            this.lblPlataforma.TabIndex = 12;
            this.lblPlataforma.Text = "Plataforma";
            // 
            // _plataforma
            // 
            this._plataforma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._plataforma.Location = new System.Drawing.Point(353, 149);
            this._plataforma.Name = "_plataforma";
            this._plataforma.Size = new System.Drawing.Size(279, 20);
            this._plataforma.TabIndex = 13;
            // 
            // _grid
            // 
            this._grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccionar,
            this.Acuerdo,
            this.Descripcion,
            this.Estado,
            this.FechaLimite,
            this.ResponsableId});
            this._grid.Location = new System.Drawing.Point(12, 201);
            this._grid.Name = "_grid";
            this._grid.RowHeadersVisible = false;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(620, 170);
            this._grid.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Acuerdos por seguir";
            // 
            // _cancelar
            // 
            this._cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelar.Location = new System.Drawing.Point(557, 377);
            this._cancelar.Name = "_cancelar";
            this._cancelar.Size = new System.Drawing.Size(75, 23);
            this._cancelar.TabIndex = 15;
            this._cancelar.Text = "Cancelar";
            this._cancelar.UseVisualStyleBackColor = true;
            // 
            // _crear
            // 
            this._crear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._crear.Location = new System.Drawing.Point(431, 377);
            this._crear.Name = "_crear";
            this._crear.Size = new System.Drawing.Size(120, 23);
            this._crear.TabIndex = 15;
            this._crear.Text = "Crear seguimiento";
            this._crear.UseVisualStyleBackColor = true;
            // 
            // Seleccionar
            // 
            this.Seleccionar.DataPropertyName = "Seleccionar";
            this.Seleccionar.HeaderText = "Incluir";
            this.Seleccionar.Name = "Seleccionar";
            this.Seleccionar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Seleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Seleccionar.Width = 60;
            // 
            // Acuerdo
            // 
            this.Acuerdo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Acuerdo.DataPropertyName = "Acuerdo";
            this.Acuerdo.HeaderText = "Acuerdo";
            this.Acuerdo.MinimumWidth = 180;
            this.Acuerdo.Name = "Acuerdo";
            this.Acuerdo.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.MinimumWidth = 200;
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Width = 65;
            // 
            // FechaLimite
            // 
            this.FechaLimite.DataPropertyName = "FechaLimite";
            dataGridViewCellStyle1.Format = "d";
            this.FechaLimite.DefaultCellStyle = dataGridViewCellStyle1;
            this.FechaLimite.HeaderText = "Fecha límite";
            this.FechaLimite.Name = "FechaLimite";
            this.FechaLimite.ReadOnly = true;
            this.FechaLimite.Width = 83;
            // 
            // ResponsableId
            // 
            this.ResponsableId.DataPropertyName = "ResponsableId";
            this.ResponsableId.HeaderText = "ID responsable";
            this.ResponsableId.Name = "ResponsableId";
            this.ResponsableId.ReadOnly = true;
            this.ResponsableId.Width = 95;
            // 
            // MinutaSeguimientoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 412);
            this.Controls.Add(this._crear);
            this.Controls.Add(this._cancelar);
            this.Controls.Add(this._grid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPlataforma);
            this.Controls.Add(this._plataforma);
            this.Controls.Add(this._hora);
            this.Controls.Add(this._fecha);
            this.Controls.Add(this.lblUbicacion);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this._lugar);
            this.Controls.Add(this._objetivo);
            this.Controls.Add(this.lblObjetivo);
            this.Controls.Add(this._titulo);
            this.Controls.Add(this.lblTitulo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "MinutaSeguimientoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MinutaSeguimientoForm";
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _titulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox _objetivo;
        private System.Windows.Forms.Label lblObjetivo;
        private System.Windows.Forms.DateTimePicker _hora;
        private System.Windows.Forms.DateTimePicker _fecha;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox _lugar;
        private System.Windows.Forms.Label lblPlataforma;
        private System.Windows.Forms.TextBox _plataforma;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _cancelar;
        private System.Windows.Forms.Button _crear;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acuerdo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaLimite;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResponsableId;
    }
}