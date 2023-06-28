using MvcMovie.Models;
using Newtonsoft.Json;

namespace WebApplication2.Client
{
    public class TranslateClient
    {

        private HttpClient _httpClient;
        private static string _address;

        public TranslateClient()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        
        public async Task<TranslationRequest> TranslateAsync(string text, string from, string to)
        {
            string inputString = text;
            string outputString = ProcessString(inputString);
            static string ProcessString(string input)
            {
                string output = "";

                foreach (char c in input)
                {
                    if (IsEnglishAlphabetCharacter(c))
                    {
                        output += c;
                    }
                    else if (c == ' ')
                    {
                        output += "%20";
                    }
                    else
                    {
                        output += Uri.EscapeDataString(c.ToString());
                    }
                }
                return output;
            }
            static bool IsEnglishAlphabetCharacter(char c)
            {
                return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
            }
            from.ToLower();
            to.ToLower();
            var response = await _httpClient.GetAsync($"/Translate?TEXT={outputString}&from={from}&to={to}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<TranslationRequest>(content);
            return responseObject;

        }
    }
}
