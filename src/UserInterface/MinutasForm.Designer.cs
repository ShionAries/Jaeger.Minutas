namespace Jaeger.Minutas.UserInterface {
    partial class MinutasForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinutasForm));
            this.ToolBar = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnSeguimiento = new System.Windows.Forms.ToolStripButton();
            this.btnTrazabilidad = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.EstadoStrip = new System.Windows.Forms.StatusStrip();
            this.gridMinutas = new System.Windows.Forms.DataGridView();
            this.btnAuditar = new System.Windows.Forms.ToolStripButton();
            this.ToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMinutas)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnEditar,
            this.btnEliminar,
            this.btnActualizar,
            this.btnSeguimiento,
            this.btnTrazabilidad,
            this.btnAuditar,
            this.btnCerrar});
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(800, 25);
            this.ToolBar.TabIndex = 0;
            this.ToolBar.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(45, 22);
            this.btnNuevo.Text = "Nueva";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(41, 22);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(54, 22);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(63, 22);
            this.btnActualizar.Text = "Actualizar";
            // 
            // btnSeguimiento
            // 
            this.btnSeguimiento.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSeguimiento.Image = ((System.Drawing.Image)(resources.GetObject("btnSeguimiento.Image")));
            this.btnSeguimiento.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSeguimiento.Name = "btnSeguimiento";
            this.btnSeguimiento.Size = new System.Drawing.Size(108, 22);
            this.btnSeguimiento.Text = "Crear seguimiento";
            this.btnSeguimiento.Click += new System.EventHandler(this.btnSeguimiento_Click);
            // 
            // btnTrazabilidad
            // 
            this.btnTrazabilidad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnTrazabilidad.Image = ((System.Drawing.Image)(resources.GetObject("btnTrazabilidad.Image")));
            this.btnTrazabilidad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTrazabilidad.Name = "btnTrazabilidad";
            this.btnTrazabilidad.Size = new System.Drawing.Size(73, 22);
            this.btnTrazabilidad.Text = "Trazabilidad";
            this.btnTrazabilidad.Click += new System.EventHandler(this.btnTrazabilidad_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(43, 22);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // EstadoStrip
            // 
            this.EstadoStrip.Location = new System.Drawing.Point(0, 428);
            this.EstadoStrip.Name = "EstadoStrip";
            this.EstadoStrip.Size = new System.Drawing.Size(800, 22);
            this.EstadoStrip.SizingGrip = false;
            this.EstadoStrip.TabIndex = 1;
            this.EstadoStrip.Text = "statusStrip1";
            // 
            // gridMinutas
            // 
            this.gridMinutas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMinutas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMinutas.Location = new System.Drawing.Point(0, 25);
            this.gridMinutas.Name = "gridMinutas";
            this.gridMinutas.ReadOnly = true;
            this.gridMinutas.RowHeadersVisible = false;
            this.gridMinutas.Size = new System.Drawing.Size(800, 403);
            this.gridMinutas.TabIndex = 2;
            // 
            // btnAuditar
            // 
            this.btnAuditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAuditar.Image = ((System.Drawing.Image)(resources.GetObject("btnAuditar.Image")));
            this.btnAuditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAuditar.Name = "btnAuditar";
            this.btnAuditar.Size = new System.Drawing.Size(50, 22);
            this.btnAuditar.Text = "Auditar";
            this.btnAuditar.Click += new System.EventHandler(this.btnAuditar_Click);
            // 
            // MinutasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridMinutas);
            this.Controls.Add(this.EstadoStrip);
            this.Controls.Add(this.ToolBar);
            this.Name = "MinutasForm";
            this.Text = "Minutas";
            this.ToolBar.ResumeLayout(false);
            this.ToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMinutas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolBar;
        private System.Windows.Forms.StatusStrip EstadoStrip;
        private System.Windows.Forms.DataGridView gridMinutas;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.ToolStripButton btnSeguimiento;
        private System.Windows.Forms.ToolStripButton btnTrazabilidad;
        private System.Windows.Forms.ToolStripButton btnAuditar;
    }
}