namespace Jaeger.Minutas.UserInterface {
    partial class MinutaTrazabilidadForm {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContenedor = new System.Windows.Forms.SplitContainer();
            this._grid = new System.Windows.Forms.DataGridView();
            this._tareas = new System.Windows.Forms.DataGridView();
            this.Relacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nivel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Titulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaReunion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinutaId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolioOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcuerdoOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TareaOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolioDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcuerdoDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TareaDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenedor)).BeginInit();
            this.splitContenedor.Panel1.SuspendLayout();
            this.splitContenedor.Panel2.SuspendLayout();
            this.splitContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._tareas)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContenedor
            // 
            this.splitContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenedor.Location = new System.Drawing.Point(0, 0);
            this.splitContenedor.Name = "splitContenedor";
            this.splitContenedor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContenedor.Panel1
            // 
            this.splitContenedor.Panel1.Controls.Add(this._grid);
            // 
            // splitContenedor.Panel2
            // 
            this.splitContenedor.Panel2.Controls.Add(this._tareas);
            this.splitContenedor.Size = new System.Drawing.Size(709, 341);
            this.splitContenedor.SplitterDistance = 112;
            this.splitContenedor.TabIndex = 0;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Relacion,
            this.Nivel,
            this.Folio,
            this.Titulo,
            this.FechaReunion,
            this.Estado,
            this.MinutaId});
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(709, 112);
            this._grid.TabIndex = 0;
            // 
            // _tareas
            // 
            this._tareas.AllowUserToAddRows = false;
            this._tareas.AllowUserToDeleteRows = false;
            this._tareas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this._tareas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._tareas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FolioOrigen,
            this.AcuerdoOrigen,
            this.TareaOrigen,
            this.EstadoOrigen,
            this.FolioDestino,
            this.AcuerdoDestino,
            this.TareaDestino,
            this.EstadoDestino});
            this._tareas.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tareas.Location = new System.Drawing.Point(0, 0);
            this._tareas.Name = "_tareas";
            this._tareas.ReadOnly = true;
            this._tareas.RowHeadersVisible = false;
            this._tareas.Size = new System.Drawing.Size(709, 225);
            this._tareas.TabIndex = 0;
            // 
            // Relacion
            // 
            this.Relacion.DataPropertyName = "Relacion";
            this.Relacion.FillWeight = 75F;
            this.Relacion.HeaderText = "Relación";
            this.Relacion.Name = "Relacion";
            this.Relacion.ReadOnly = true;
            // 
            // Nivel
            // 
            this.Nivel.DataPropertyName = "Nivel";
            this.Nivel.FillWeight = 35F;
            this.Nivel.HeaderText = "Nivel";
            this.Nivel.Name = "Nivel";
            this.Nivel.ReadOnly = true;
            // 
            // Folio
            // 
            this.Folio.DataPropertyName = "Folio";
            this.Folio.FillWeight = 70F;
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            // 
            // Titulo
            // 
            this.Titulo.DataPropertyName = "Titulo";
            this.Titulo.FillWeight = 180F;
            this.Titulo.HeaderText = "Título";
            this.Titulo.Name = "Titulo";
            this.Titulo.ReadOnly = true;
            // 
            // FechaReunion
            // 
            this.FechaReunion.DataPropertyName = "FechaReunion";
            dataGridViewCellStyle3.Format = "d";
            this.FechaReunion.DefaultCellStyle = dataGridViewCellStyle3;
            this.FechaReunion.FillWeight = 75F;
            this.FechaReunion.HeaderText = "Fecha";
            this.FechaReunion.Name = "FechaReunion";
            this.FechaReunion.ReadOnly = true;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.FillWeight = 65F;
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            // 
            // MinutaId
            // 
            this.MinutaId.DataPropertyName = "MinutaId";
            this.MinutaId.FillWeight = 35F;
            this.MinutaId.HeaderText = "Id";
            this.MinutaId.Name = "MinutaId";
            this.MinutaId.ReadOnly = true;
            // 
            // FolioOrigen
            // 
            this.FolioOrigen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FolioOrigen.DataPropertyName = "FolioOrigen";
            this.FolioOrigen.FillWeight = 95F;
            this.FolioOrigen.HeaderText = "Folio origen";
            this.FolioOrigen.Name = "FolioOrigen";
            this.FolioOrigen.Width = 79;
            // 
            // AcuerdoOrigen
            // 
            this.AcuerdoOrigen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AcuerdoOrigen.DataPropertyName = "AcuerdoOrigen";
            this.AcuerdoOrigen.HeaderText = "Acuerdo original";
            this.AcuerdoOrigen.MinimumWidth = 150;
            this.AcuerdoOrigen.Name = "AcuerdoOrigen";
            // 
            // TareaOrigen
            // 
            this.TareaOrigen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TareaOrigen.DataPropertyName = "TareaOrigen";
            this.TareaOrigen.HeaderText = "Tarea original";
            this.TareaOrigen.MinimumWidth = 150;
            this.TareaOrigen.Name = "TareaOrigen";
            // 
            // EstadoOrigen
            // 
            this.EstadoOrigen.DataPropertyName = "EstadoOrigen";
            this.EstadoOrigen.HeaderText = "Estado origen";
            this.EstadoOrigen.Name = "EstadoOrigen";
            this.EstadoOrigen.Width = 89;
            // 
            // FolioDestino
            // 
            this.FolioDestino.DataPropertyName = "FolioDestino";
            this.FolioDestino.HeaderText = "Folio seguimiento";
            this.FolioDestino.Name = "FolioDestino";
            this.FolioDestino.Width = 104;
            // 
            // AcuerdoDestino
            // 
            this.AcuerdoDestino.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AcuerdoDestino.DataPropertyName = "AcuerdoDestino";
            this.AcuerdoDestino.HeaderText = "Acuerdo seguimiento";
            this.AcuerdoDestino.MinimumWidth = 150;
            this.AcuerdoDestino.Name = "AcuerdoDestino";
            // 
            // TareaDestino
            // 
            this.TareaDestino.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TareaDestino.HeaderText = "Tarea seguimiento";
            this.TareaDestino.MinimumWidth = 150;
            this.TareaDestino.Name = "TareaDestino";
            // 
            // EstadoDestino
            // 
            this.EstadoDestino.DataPropertyName = "EstadoDestino";
            this.EstadoDestino.HeaderText = "Estado Actual";
            this.EstadoDestino.Name = "EstadoDestino";
            this.EstadoDestino.Width = 90;
            // 
            // MinutaTrazabilidadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 341);
            this.Controls.Add(this.splitContenedor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MinutaTrazabilidadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trazabilidad de minutas y acuerdos";
            this.splitContenedor.Panel1.ResumeLayout(false);
            this.splitContenedor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContenedor)).EndInit();
            this.splitContenedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._tareas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContenedor;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.DataGridView _tareas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nivel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Titulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaReunion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinutaId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolioOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcuerdoOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn TareaOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolioDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcuerdoDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn TareaDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoDestino;
    }
}