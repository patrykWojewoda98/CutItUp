using CutItUp.Data.Context;
using CutItUp.Data.Data.CMS.GPT;
using Intranet.Models.GPTModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Intranet.Controllers
{
    public class LayoutController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly CutItUpContext _context;
        public LayoutController(IConfiguration configuration, CutItUpContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string message, string returnUrl)
        {
            Console.WriteLine("Message: " + message);
            HttpClient client = new HttpClient();
            string openAiApiKey = _configuration["OpenAI:ApiKey"];
            string apiUrl = "https://api.openai.com/v1/chat/completions";
            string requestBody = $@"
{{
    ""model"": ""gpt-4o-mini"",
    ""messages"": [
        {{
            ""role"": ""user"",
            ""content"": ""{message}"",
            ""max_output_tokens"": 100
        }}
    ]
}}";

            GPTMessage gptMessage = new GPTMessage();
            gptMessage.IsUsersMesssage = true;
            gptMessage.Content = message;
            _context.GPTMessage.Add(gptMessage);
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

                            _context.GPTMessage.Add(new GPTMessage { IsUsersMesssage = false, Content = data.choices[0].message.content });
                        }
                        
                        
                    }
                    else
                    {
                        _context.GPTMessage.Add(new GPTMessage { IsUsersMesssage = false, Content = "Błąd: " + response.StatusCode.ToString() });

                    }
                }
                catch (HttpRequestException e)
                {
                    _context.GPTMessage.Add(new GPTMessage { IsUsersMesssage = false, Content = "Błąd: " + e.Message });
                }
                await _context.SaveChangesAsync();
            }
            return Redirect(returnUrl);
        }
    }
}
