using Intranet.Models;
using Intranet.Models.GPTModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Intranet.Controllers
{
    public class LayoutController : Controller
    {
        private readonly IConfiguration _configuration;
        public LayoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageModel model, string returnUrl)
        {
            HttpClient client = new HttpClient();
            string openAiApiKey = _configuration["OpenAI:ApiKey"];
            string apiUrl = "https://api.openai.com/v1/chat/completions";
            string requestBody = $@"
{{
    ""model"": ""gpt-4o-mini"",
    ""messages"": [
        {{
            ""role"": ""user"",
            ""content"": ""{model.Message}"",
            ""max_output_tokens"": 100
        }}
    ]
}}";

            model.IsUsersMessage = true;
            MessageModel.messages.Add(model);
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl)
                {
                    Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                };

                request.Headers.Add("Authorization", $"Bearer {openAiApiKey}");

                try
                {
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        var data = JsonConvert.DeserializeObject<ResponseFromGPTModel>(responseString);
                        if (data.choices[0] != null)
                        {
                            MessageModel.messages.Add(new MessageModel { Message = data.choices[0].message.content, IsUsersMessage = false });
                        }
                        
                        
                    }
                    else
                    {
                        MessageModel.messages.Add(new MessageModel { Message = "Błąd: " + response.StatusCode.ToString(), IsUsersMessage = false });

                    }
                }
                catch (HttpRequestException e)
                {
                    MessageModel.messages.Add(new MessageModel { Message = "Błąd: " + e.Message, IsUsersMessage = false });
                }
            }
            return Redirect(returnUrl);
        }
    }
}
