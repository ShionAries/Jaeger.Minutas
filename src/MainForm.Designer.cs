
namespace Jaeger.Minutas {
    partial class MainForm {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent() {
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.opSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdmon = new System.Windows.Forms.ToolStripMenuItem();
            this.opUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.opMinutas = new System.Windows.Forms.ToolStripMenuItem();
            this.EstadoStrip = new System.Windows.Forms.StatusStrip();
            this.lblEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMain.SuspendLayout();
            this.EstadoStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuArchivo,
            this.menuAdmon});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuArchivo
            // 
            this.menuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opSalir});
            this.menuArchivo.Name = "menuArchivo";
            this.menuArchivo.Size = new System.Drawing.Size(60, 20);
            this.menuArchivo.Text = "Archivo";
            // 
            // opSalir
            // 
            this.opSalir.Name = "opSalir";
            this.opSalir.Size = new System.Drawing.Size(96, 22);
            this.opSalir.Text = "Salir";
            // 
            // menuAdmon
            // 
            this.menuAdmon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opUsuarios,
            this.opMinutas});
            this.menuAdmon.Name = "menuAdmon";
            this.menuAdmon.Size = new System.Drawing.Size(100, 20);
            this.menuAdmon.Text = "Administración";
            // 
            // opUsuarios
            // 
            this.opUsuarios.Name = "opUsuarios";
            this.opUsuarios.Size = new System.Drawing.Size(119, 22);
            this.opUsuarios.Text = "Usuarios";
            // 
            // opMinutas
            // 
            this.opMinutas.Name = "opMinutas";
            this.opMinutas.Size = new System.Drawing.Size(119, 22);
            this.opMinutas.Text = "Minutas";
            this.opMinutas.Click += new System.EventHandler(this.opMinutas_Click);
            // 
            // EstadoStrip
            // 
            this.EstadoStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEstado});
            this.EstadoStrip.Location = new System.Drawing.Point(0, 428);
            this.EstadoStrip.Name = "EstadoStrip";
            this.EstadoStrip.Size = new System.Drawing.Size(800, 22);
            this.EstadoStrip.TabIndex = 1;
            this.EstadoStrip.Text = "statusStrip1";
            // 
            // lblEstado
            // 
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(16, 17);
            this.lblEstado.Text = "...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EstadoStrip);
            this.Controls.Add(this.menuMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.MinimumSize = new System.Drawing.Size(650, 320);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de minutas";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.EstadoStrip.ResumeLayout(false);
            this.EstadoStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.StatusStrip EstadoStrip;
        private System.Windows.Forms.ToolStripMenuItem menuArchivo;
        private System.Windows.Forms.ToolStripMenuItem opSalir;
        private System.Windows.Forms.ToolStripMenuItem menuAdmon;
        private System.Windows.Forms.ToolStripMenuItem opUsuarios;
        private System.Windows.Forms.ToolStripMenuItem opMinutas;
        private System.Windows.Forms.ToolStripStatusLabel lblEstado;
    }
}

