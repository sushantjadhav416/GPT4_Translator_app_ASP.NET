using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebApplication2.Models;
using WebApplication2.DTOs;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        private readonly List<String> MostUsedLanguage = new List<String>()
        {
            "English",
            "Mandarin Chinese",
            "Hindi",
            "Spanish",
            "French",
            "Modern Standard Arabic",
            "Portuguese",
            "Russian",
            "Urdu",
            "Indonesian",
            "German",
            "Japanese",
            "Nigerian Pidgin",
            "Marathi",
            "Korean",
            "Italian"
        };

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            ViewBag.Languages = new SelectList(MostUsedLanguage);
            return View();
        }

        public async Task<IActionResult> OpenAIGPT(string query,string selectedLanguage)
        {
            //Get openapi key from appsetting.json

            var openAPIkey = _configuration["OpenAI:ApiKey"];

            //Setup HttpClient with openAI Key
            _httpClient.DefaultRequestHeaders.Add("Authorization",$"Bearer {openAPIkey}");

            //Define the request Payload
            var payload = new
            {
                model = "gpt-4",
                messages = new object[]
                {
                    new { role = "system", content = $"Translate to {selectedLanguage}" },
                    new { role = "user", content = query }
                },
                temperature = 0,
                max_tokens = 256

            };

            string jsonPayload = JsonConvert.SerializeObject(payload);

            HttpContent httpcontent =  new StringContent(jsonPayload,Encoding.UTF8,"application/json");
             
            //Send the request
            var responseMessage = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions",httpcontent);

            var responseMessageJson = await responseMessage.Content.ReadAsStringAsync();

            //Return Response
            var Response = JsonConvert.DeserializeObject<OpenAIResponse>(responseMessageJson);

            ViewBag.Result = Response.choices[0].Message.Content;



            return View();
        }

      

        
    }
}