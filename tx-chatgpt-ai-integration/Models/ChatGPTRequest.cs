namespace tx_chatgpt_ai_integration.Models {
    public class ChatGPTRequest {
      public string Text { get; set; }
      public string Type { get; set; }
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
      public string OPENAI_API_KEY = "";

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

