using MvcMovie.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication2.Client
{
    public class YTClient
    {
        private HttpClient _httpClient;
        private static string _address;

        public YTClient()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<ApiResponseYT> YTAsync(string text)
        {
            string restext = "";
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
            var response = await _httpClient.GetAsync($"/YouTube?TEXT={restext}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<ApiResponseYT>(content);
            return responseObject;

        }
    }
}
