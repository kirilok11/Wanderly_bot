using MvcMovie.Models;
using Newtonsoft.Json;

namespace WebApplication2.Client
{
    public class WikiClient
    {
        private HttpClient _httpClient;
        private static string _address;

        public WikiClient()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<WikiResponse> WikiAsync(string text, int NumOfArticles)
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
            var response = await _httpClient.GetAsync($"/Wiki?TEXT={restext}&NumberOfArticles={NumOfArticles}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<WikiResponse>(content);
            return responseObject;

        }
    }
}
