using MvcMovie.Models;
using Newtonsoft.Json;

namespace WebApplication2.Client
{
    public class HistoricalPlacesClient
    {
        private HttpClient _httpClient;
        private static string _address;

        public HistoricalPlacesClient()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<Root1> HistoricalPlacesAroundAsync(string lat, string lon, string rad, int num)
        {
            
            var response = await _httpClient.GetAsync($"/NearBY?LAT={lat}&LON={lon}&RAD={rad}&LANG=en&NUM={num}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<Root1>(content);
            return responseObject;

        }
    }
}
