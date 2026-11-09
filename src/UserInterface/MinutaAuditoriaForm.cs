using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaeger.Minutas.Aplication.Services;

namespace Jaeger.Minutas.UserInterface {
    public partial class MinutaAuditoriaForm : Form {
        private readonly MinutaAuditoriaService _service;
        private readonly BindingSource _binding = new BindingSource();
        public MinutaAuditoriaForm() {
            InitializeComponent();
        }
    }
}
