using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jaeger.Minutas.Aplication.DependencyInjection;
using Jaeger.Minutas.Domain.Entities;

namespace Jaeger.Minutas.UserInterface {
    /// <summary>Genera la vista preliminar y la impresión de una minuta.</summary>
    internal sealed class MinutaImpresion : IDisposable {
        private readonly PrintDocument _documento;
        private readonly List<string> _lineas;
        private readonly Font _fuente = new Font("Arial", 9f);
        private readonly Font _fuenteTitulo = new Font("Arial", 16f, FontStyle.Bold);
        private readonly Font _fuenteSeccion = new Font("Arial", 10f, FontStyle.Bold);
        private readonly Font _fuentePie = new Font("Arial", 8f);
        private int _indiceLinea;
        private int _pagina;

        private MinutaImpresion(Minuta minuta, MinutaDetalle detalle, IList<UsuarioCorporativo> usuarios) {
            _lineas = CrearContenido(minuta, detalle, usuarios);
            _documento = new PrintDocument {
                DocumentName = "Minuta " + (minuta.Folio ?? minuta.IdMinuta.ToString()),
                DefaultPageSettings = { PaperSize = ObtenerCarta(), Margins = new Margins(55, 55, 55, 55) }
            };
            _documento.PrintPage += ImprimirPagina;
        }

        public static async Task MostrarAsync(IWin32Window owner, MinutasComposition composition, long minutaId) {
            if (composition == null) throw new ArgumentNullException(nameof(composition));
            Minuta minuta = await composition.ObtenerMinutaService.EjecutarAsync(minutaId);
            if (minuta == null) throw new InvalidOperationException("No se encontró la minuta seleccionada.");
            MinutaDetalle detalle = await composition.MinutaDetalleService.ObtenerAsync(minutaId);
            IList<UsuarioCorporativo> usuarios = await composition.UsuarioCorporativoService.ObtenerActivosAsync();
            using (var impresion = new MinutaImpresion(minuta, detalle, usuarios))
            using (var vista = new PrintPreviewDialog { Document = impresion._documento, Width = 1000, Height = 750, StartPosition = FormStartPosition.CenterParent }) {
                vista.ShowDialog(owner);
            }
        }

        private static PaperSize ObtenerCarta() {
            return new PaperSize("Carta", 850, 1100);
        }

        private static List<string> CrearContenido(Minuta m, MinutaDetalle d, IList<UsuarioCorporativo> usuarios) {
            var resultado = new List<string>();
            Func<long, string> nombreUsuario = id => {
                UsuarioCorporativo usuario = usuarios.FirstOrDefault(x => x.Id == id);
                return usuario == null ? "Usuario " + id : usuario.NombreMostrar;
            };
            resultado.Add("MINUTA DE REUNIÓN");
            resultado.Add("Folio: " + Valor(m.Folio) + "     Estado: " + m.Estado);
            resultado.Add("Título: " + Valor(m.Titulo));
            resultado.Add("Tipo: " + Valor(m.Tipo) + "     Fecha: " + m.FechaReunion.ToString("dd/MM/yyyy"));
            resultado.Add("Horario: " + m.HoraInicio.ToString(@"hh\:mm") + " - " + (m.HoraFin.HasValue ? m.HoraFin.Value.ToString(@"hh\:mm") : "No definido"));
            resultado.Add("Lugar: " + Valor(m.Lugar) + "     Plataforma: " + Valor(m.Plataforma));
            resultado.Add("OBJETIVO");
            resultado.Add(Valor(m.Objetivo));

            resultado.Add("ASISTENTES");
            if (d.Asistentes.Count == 0) resultado.Add("No hay asistentes registrados.");
            foreach (MinutaAsistente a in d.Asistentes)
                resultado.Add("• " + Valor(a.Nombre) + " | " + Valor(a.Rol) + " | Asistencia: " + Valor(a.EstadoAsistencia) + (string.IsNullOrWhiteSpace(a.Correo) ? string.Empty : " | " + a.Correo));

            resultado.Add("TEMAS TRATADOS");
            if (d.Temas.Count == 0) resultado.Add("No hay temas registrados.");
            foreach (MinutaTema t in d.Temas.OrderBy(x => x.Orden)) {
                resultado.Add(t.Orden + ". " + Valor(t.Tema) + " | Estado: " + Valor(t.Estado));
                if (!string.IsNullOrWhiteSpace(t.Descripcion)) resultado.Add("   " + t.Descripcion);
                if (t.ResponsableId.HasValue) resultado.Add("   Responsable: " + nombreUsuario(t.ResponsableId.Value));
            }

            resultado.Add("ACUERDOS");
            if (d.Acuerdos.Count == 0) resultado.Add("No hay acuerdos registrados.");
            foreach (MinutaAcuerdo a in d.Acuerdos.OrderBy(x => x.Orden))
                resultado.Add(a.Orden + ". [" + Valor(a.Tipo) + "] " + Valor(a.Descripcion));

            resultado.Add("TAREAS Y COMPROMISOS");
            if (d.Tareas.Count == 0) resultado.Add("No hay tareas registradas.");
            foreach (MinutaTarea t in d.Tareas.OrderBy(x => x.FechaLimite)) {
                resultado.Add("• " + Valor(t.Descripcion));
                resultado.Add("   Responsable: " + nombreUsuario(t.ResponsableId) + " | Fecha límite: " + t.FechaLimite.ToString("dd/MM/yyyy") + " | Estado: " + Valor(t.Estado));
                if (!string.IsNullOrWhiteSpace(t.Evidencia)) resultado.Add("   Evidencia: " + t.Evidencia);
            }

            resultado.Add("PRÓXIMA REUNIÓN");
            resultado.Add(m.ProximaFecha.HasValue ? "Fecha: " + m.ProximaFecha.Value.ToString("dd/MM/yyyy") + " | Lugar: " + Valor(m.ProximaLugar) : "No se ha programado una próxima reunión.");
            resultado.Add(" ");
            resultado.Add("Documento generado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            return resultado;
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e) {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float ancho = e.MarginBounds.Width;
            _pagina++;
            while (_indiceLinea < _lineas.Count) {
                string linea = _lineas[_indiceLinea];
                bool titulo = _indiceLinea == 0;
                bool seccion = EsSeccion(linea);
                Font fuente = titulo ? _fuenteTitulo : seccion ? _fuenteSeccion : _fuente;
                float alto = titulo ? 30f : seccion ? 23f : 17f;
                SizeF medida = e.Graphics.MeasureString(linea, fuente, (int)ancho);
                alto = Math.Max(alto, medida.Height + 2f);
                if (y + alto > e.MarginBounds.Bottom - 22) break;
                if (seccion && !titulo) y += 7;
                e.Graphics.DrawString(linea, fuente, Brushes.Black, new RectangleF(x, y, ancho, alto));
                y += alto;
                _indiceLinea++;
            }
            e.Graphics.DrawLine(Pens.Gray, e.MarginBounds.Left, e.MarginBounds.Bottom - 15, e.MarginBounds.Right, e.MarginBounds.Bottom - 15);
            e.Graphics.DrawString("Jaeger Minutas", _fuentePie, Brushes.Gray, x, e.MarginBounds.Bottom - 12);
            e.Graphics.DrawString("Página " + _pagina, _fuentePie, Brushes.Gray, e.MarginBounds.Right - 55, e.MarginBounds.Bottom - 12);
            e.HasMorePages = _indiceLinea < _lineas.Count;
            if (!e.HasMorePages) { _indiceLinea = 0; _pagina = 0; }
        }

        private static bool EsSeccion(string texto) {
            return texto == "OBJETIVO" || texto == "ASISTENTES" || texto == "TEMAS TRATADOS" || texto == "ACUERDOS" || texto == "TAREAS Y COMPROMISOS" || texto == "PRÓXIMA REUNIÓN";
        }

        private static string Valor(string texto) { return string.IsNullOrWhiteSpace(texto) ? "No especificado" : texto.Trim(); }

        public void Dispose() {
            _documento.Dispose();
            _fuente.Dispose();
            _fuenteTitulo.Dispose();
            _fuenteSeccion.Dispose();
            _fuentePie.Dispose();
        }
    }
}
