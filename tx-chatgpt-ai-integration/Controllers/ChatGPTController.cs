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

namespace tx_chatgpt_ai_integration.Controllers {
    public class ChatGPTController : Controller {

      [HttpPost]
      public string RequestAPI([FromBody] ChatGPTRequest request) {

         HttpClient client = new HttpClient();  

         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

         var openAIPrompt = new {
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

      
    }
}
