using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;

namespace PowerPointBIBVSTO
{
    public partial class ucChatGPTPanel : UserControl
    {
        public static string UltimaRespuesta { get; private set; } = string.Empty;
        private string apiKey;

        public ucChatGPTPanel()
        {
            InitializeComponent();

            btnEnviar1.Click += btnEnviar1_Click;
            txtApiKey.TextChanged += (s, e) => apiKey = txtApiKey.Text;
            btnValidar.Click += btnValidar_Click;

        }

        private async void btnEnviar1_Click(object sender, EventArgs e)
        {
            string prompt = txtPregunta1.Text.Trim();
            if (string.IsNullOrWhiteSpace(prompt) || string.IsNullOrWhiteSpace(apiKey))
            {
                MessageBox.Show("Por favor ingresa el texto y la API Key.");
                return;
            }

            txtPregunta1.Text = "";

            // Mostrar mensaje del usuario
            AgregarBurbuja("Tú", prompt, Color.LightBlue, ContentAlignment.MiddleRight);

            try
            {
                string respuesta = await ConsultarChatGPT(prompt);
                UltimaRespuesta = respuesta;

                // Mostrar respuesta de GPT
                AgregarBurbuja("GPT", respuesta, Color.LightGray, ContentAlignment.MiddleLeft);
            }
            catch (Exception ex)
            {
                AgregarBurbuja("Error", $"❌ {ex.Message}", Color.MistyRose, ContentAlignment.MiddleLeft);
            }
        }

        private void AgregarBurbuja(string remitente, string texto, Color fondo, ContentAlignment alineacion)
        {
            var panel = new Panel
            {
                Width = chatPanel.ClientSize.Width - 40,
                AutoSize = true,
                Margin = new Padding(10),
                BackColor = fondo
            };

            var label = new Label
            {
                Text = $"{remitente}:",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(5)
            };

            var mensaje = new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 9),
                MaximumSize = new Size(panel.Width - 20, 0),
                AutoSize = true,
                Padding = new Padding(5),
                TextAlign = alineacion
            };

            panel.Controls.Add(mensaje);
            panel.Controls.Add(label);
            chatPanel.Controls.Add(panel);
            chatPanel.ScrollControlIntoView(panel);
        }

        private async Task<string> ConsultarChatGPT(string prompt)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiKey);

                var payload = new
                {
                    model = "gpt-4",
                    messages = new[] { new { role = "user", content = prompt } },
                    temperature = 0.7
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string resultJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"OpenAI error: {resultJson}");

                using (var doc = JsonDocument.Parse(resultJson))
                {
                    return doc.RootElement
                              .GetProperty("choices")[0]
                              .GetProperty("message")
                              .GetProperty("content")
                              .GetString();
                }
            }
        }
        private void btnValidar_Click(object sender, EventArgs e)
        {
            apiKey = txtApiKey.Text.Trim();

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                MessageBox.Show("La API Key no puede estar vacía.", "Validación fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("API Key validada correctamente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
            
        private void ucChatGPTPanel_Load(object sender, EventArgs e) { }
        private void txtApiKey_TextChanged(object sender, EventArgs e) { }
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void chatPanel_Paint(object sender, PaintEventArgs e) { }
        private void LayoutPrincipal_Paint(object sender, PaintEventArgs e) { }
    }
}
