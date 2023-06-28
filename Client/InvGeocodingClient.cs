using Newtonsoft.Json;
using static MvcMovie.Models.GeometryData;

namespace WebApplication2.Client
{
    public class InvGeocodingClient
    {
        private HttpClient _httpClient;
        private static string _address;

        public InvGeocodingClient()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<Root> InvGeocodingAsync(string lat, string lon)
        {
            
            var response = await _httpClient.GetAsync($"/InvGeocoding?lat={lat}&lon={lon}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<Root>(content);
            return responseObject;

        }
    }
}
