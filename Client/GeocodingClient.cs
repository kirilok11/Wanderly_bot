using Microsoft.AspNetCore.Mvc.Infrastructure;
using MvcMovie.Models;
using Newtonsoft.Json;
using static MvcMovie.Models.GeometryData;

namespace WebApplication2.Client
{
    public class GeocodingClient
    {
        private HttpClient _httpClient;
        private static string _address;

        public GeocodingClient()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<GeocodingResponse> GeocodingAsync(string text)
        {
            string restext = "";
            for (int c = 0; c < text.Length; c++)
            {
                if (text[c] == ' ')
                {
                    restext = restext + "%2C";
                }
                else
                {
                    restext = restext + text[c];
                }

            }
            var response = await _httpClient.GetAsync($"/Geocoding?lng=en&address={restext}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<GeocodingResponse>(content);
            return responseObject;

        }
    }
}
