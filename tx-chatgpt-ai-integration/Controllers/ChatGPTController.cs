using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

using System.Text.RegularExpressions;
using System.Text;
using tx_chatgpt_ai_integration.Models;
using Newtonsoft.Json;
using System.Security.Principal;
using TXTextControl.Web;

namespace tx_chatgpt_ai_integration.Controllers
{
    public class ChatGPTController : Controller
    {

        [HttpPost]
        public string RequestAPI([FromBody] ChatGPTRequest request)
        {

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Models.Constants.OPENAI_API_KEY);

            var openAIPrompt = new
            {
                model = "text-davinci-003",
                prompt = Models.Constants.Prompts[request.Type] + request.Text.Trim(),
                temperature = 0.2,
                max_tokens = 2048,
                top_p = 1
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(openAIPrompt), Encoding.UTF8, "application/json");
            var result = client.PostAsync("https://api.openai.com/v1/completions", stringContent).Result;
            var jsonContent = result.Content.ReadAsStringAsync().Result;
            ChatGPTResponse chatGPTResponse = JsonConvert.DeserializeObject<ChatGPTResponse>(jsonContent);

            return chatGPTResponse.Choices[0].Text.Trim();
        }

        [HttpPost]
        public string PromptRequest([FromBody] ChatGPTRequest request)
        {

            var htmlSuffix = ". Create the results in formatted HTML format with h1, p and li elements.";

            // Remove a trailing full stop, if present
            if (request.Text.EndsWith("."))
            {
                request.Text = request.Text.TrimEnd('.');
            }

            HttpClient client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", Models.Constants.OPENAI_API_KEY)
                }
            };

            Request apiRequest = new Request
            {
                Messages = new[]
                {
                    new RequestMessage
                    {
                         Role = "system",
                         Content = "You are a helpful assistant."
                    },
                    new RequestMessage
                    {
                         Role = "user",
                         Content = request.Text + htmlSuffix
                    }
                }
            };

            StringContent content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(apiRequest),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage httpResponseMessage = client.PostAsync(
                "https://api.openai.com/v1/chat/completions",
                content
            ).Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Response response = System.Text.Json.JsonSerializer.Deserialize<Response>(
                    httpResponseMessage.Content.ReadAsStringAsync().Result
                );

                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(response.Choices[0].Message.Content);
                return System.Convert.ToBase64String(plainTextBytes);
            }

            return null;


        }


    }
}
