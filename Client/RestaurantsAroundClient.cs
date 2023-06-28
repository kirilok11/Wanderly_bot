using Newtonsoft.Json;

namespace WebApplication2.Client
{
    public class RestaurantsAroundClient
    {
        private HttpClient _httpClient;
        private static string _address;

        public RestaurantsAroundClient()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<Root3> RestaurantsAroundAsync(string lat, string lon,string rad, string kind, string keyword)
        {

            var response = await _httpClient.GetAsync($"/PlacesConroller?LAT={lat}&LON={lon}&RAD={rad}&KIND={kind}&KEYWORD={keyword}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<Root3>(content);
            return responseObject;

        }
    }
}
