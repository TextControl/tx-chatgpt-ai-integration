using System.Text.Json.Serialization;

namespace tx_chatgpt_ai_integration.Models {
    public class ChatGPTRequest {
      public string Text { get; set; }
      public string Type { get; set; }
   }

    public class RequestMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public class Request
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 4000;
        [JsonPropertyName("messages")]
        public RequestMessage[] Messages { get; set; }
    }

    public class Response
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("created")]
        public int Created { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("usage")]
        public ResponseUsage Usage { get; set; }
        [JsonPropertyName("choices")]
        public ResponseChoice[] Choices { get; set; }
    }

    public class ResponseUsage
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }

    public class ResponseChoice
    {
        [JsonPropertyName("message")]
        public ResponseMessage Message { get; set; }
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }

    public class ResponseMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public class ChatGPTResponse {
      public string Id { get; set; }
      public Choice[] Choices { get; set; }  
   }

   public class Choice {
      public string Text { get; set; }
      public int Index { get; set; }   
   }

   public class Constants {
      public string OPENAI_MODEL = "text-davinci-003";
      public static string OPENAI_API_KEY = "";

      public static readonly Dictionary<string, string> Prompts = new Dictionary<string, string>() {
            { "formal", "Return the text after the colon in a more professional way and provide the results as plain text: " },
            { "informal", "Return the text after the colon in an informal way and provide the results as plain text: " },
            { "optimistic", "Return the text after the colon in an optimistic way and provide the results as plain text: " },
            { "encouraging", "Return the text after the colon that encourages the reader and provide the results as plain text: " },
            { "expand", "Add more details to increase content length to the text after the colon and provide the results as plain text: " },
            { "shorten", "Shorten the text after the colon and provide the results as plain text: " }
      };

   }
}

