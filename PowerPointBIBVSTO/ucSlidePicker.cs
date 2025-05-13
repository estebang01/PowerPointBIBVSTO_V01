using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace PowerPointBIBVSTO
{
    public partial class ucSlidePicker : UserControl
    {
        private const int CARD_MARGIN = 8;

        public ucSlidePicker()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            flpThumbs.Dock = DockStyle.Fill;
            flpThumbs.AutoScroll = true;
            this.SizeChanged += (s, e) => ResizeCards();
        }

        public void LoadTemplates(string[] files)
        {
            flpThumbs.Controls.Clear();
            var app = Globals.ThisAddIn.Application;

            foreach (string file in files.Where(f => File.Exists(f)))
            {
                var pres = app.Presentations.Open(file,
                    Office.MsoTriState.msoFalse,
                    Office.MsoTriState.msoFalse,
                    Office.MsoTriState.msoFalse);

                foreach (PowerPoint.Slide s in pres.Slides)
                {
                    string tmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
                    s.Export(tmp, "PNG", 240, 135);

                    var slideRef = new SlideRef { Path = file, Index = s.SlideIndex };

                    var card = new Panel
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                        Width = flpThumbs.ClientSize.Width - (CARD_MARGIN * 2),
                        Height = 210,
                        Margin = new Padding(CARD_MARGIN),
                        Padding = new Padding(6),
                        Tag = slideRef
                    };

                    var lbl = new Label
                    {
                        Text = Truncate(s.Name, 40),
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        ForeColor = Color.Black,
                        Dock = DockStyle.Top,
                        Height = 28,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    var img = new PictureBox
                    {
                        Image = Image.FromFile(tmp),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Fill,
                        Cursor = Cursors.Hand
                    };

                    var btn = CreateOfficeButton("Agregar", slideRef);
                    btn.Dock = DockStyle.Bottom;

                    card.Controls.Add(img);
                    card.Controls.Add(btn);
                    card.Controls.Add(lbl);

                    flpThumbs.Controls.Add(card);
                }

                pres.Close();
            }

            ResizeCards();
        }

        private void ResizeCards()
        {
            int cardWidth = Math.Max(flpThumbs.ClientSize.Width - (CARD_MARGIN * 2), 180);
            foreach (Control control in flpThumbs.Controls.OfType<Panel>())
                control.Width = cardWidth;
        }

        private Button CreateOfficeButton(string text, object tag)
        {
            Button btn = new Button
            {
                Text = text,
                Font = new Font("Segoe UI", 9),
                Height = 30,
                Tag = tag,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(223, 223, 223);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(240, 240, 240);
            btn.MouseDown += (s, e) => btn.BackColor = Color.FromArgb(190, 230, 255);
            btn.MouseUp += (s, e) => btn.BackColor = Color.FromArgb(223, 223, 223);
            btn.Click += AddSlide_Click;

            return btn;
        }

        private void AddSlide_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn) || !(btn.Tag is SlideRef info)) return;

            var app = Globals.ThisAddIn.Application;

            // Asegura que haya una presentación activa
            if (app.Presentations.Count == 0)
            {
                app.Presentations.Add(Office.MsoTriState.msoTrue);
            }

            var dest = app.ActivePresentation;

            // Validar que haya una diapositiva activa
            if (app.ActiveWindow.View.Slide == null) return;

            // Calcular el índice de inserción después de la diapositiva activa
            int insertIndex = app.ActiveWindow.View.Slide.SlideIndex + 1;
            int adjustedIndex = insertIndex - 1; // Ajustar el índice de inserción

            // Insertar la diapositiva seleccionada
            dest.Slides.InsertFromFile(info.Path, adjustedIndex, info.Index, info.Index);

            // Mostrar confirmación
            new ToolTip().Show("✅ Diapositiva insertada", btn, 0, -20, 1200);
        }


        private string Truncate(string text, int max) => text.Length <= max ? text : text.Substring(0, max - 3) + "...";

        private struct SlideRef
        {
            public string Path;
            public int Index;
        }

        public int MiniaturaCount => flpThumbs.Controls.Count;
    }
}
