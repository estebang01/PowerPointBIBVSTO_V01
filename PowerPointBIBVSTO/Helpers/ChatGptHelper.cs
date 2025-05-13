using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.Collections.Generic;
using Microsoft.VisualBasic;


namespace PowerPointBIBVSTO.Helpers

{
    public static class ChatGptHelper
    {
        private static string _userApiKey = null;
        private static readonly string endpoint = "https://api.openai.com/v1/chat/completions";

        // Llama esta función antes de usar EnviarPromptAsync si aún no hay clave
        private static bool EnsureApiKey()
        {
            if (!string.IsNullOrEmpty(_userApiKey)) return true;

            string input = Interaction.InputBox( // No es necesario cambiar esta línea
                "Por favor, introduce tu API Key de OpenAI:", "API Key Requerida", "");

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("No se proporcionó una API key. Operación cancelada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            _userApiKey = input.Trim();
            return true;
        }

        public static async Task<string> EnviarPromptAsync(string prompt)
        {
            if (!EnsureApiKey())
                return "❌ No se envió la solicitud por falta de clave.";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _userApiKey);

                var body = new
                {
                    model = "gpt-4-mini", // ← NO uses "gpt-4.1-nano"
                    messages = new[] {
                        new { role = "system", content = "Eres un banquero de inversión de Bancolombia. Responde de forma profesional y precisa." },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.7
                };

                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al consultar OpenAI:\n{result}");

                dynamic json = JsonConvert.DeserializeObject(result);
                return json.choices[0].message.content.ToString();
            }
        }
    }
}
