using Microsoft.VisualBasic;
using MvcMovie.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication2.Client
{
    
    public class GptClient
    {
        private HttpClient _httpClient;
        private static string _address;
        
        public GptClient()
        {
            _address = Constants.address;
            
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<GPTResponse> AskGPTAsync(string text)
        {
            string restext = "Answer%20no%20more%20than%20in%2020%20words";
            for (int c = 0; c < text.Length; c++)
            {
                if (text[c] == ' ')
                {
                    restext = restext + "%20";
                }
                else
                {
                    restext = restext + text[c];
                }

            }
            var response = await _httpClient.GetAsync($"/GPTcontrollercs?cont={restext}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<GPTResponse>(content);
            return responseObject;

        }
    }
}
