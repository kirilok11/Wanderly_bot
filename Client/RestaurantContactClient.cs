using MvcMovie.Models;
using Newtonsoft.Json;

namespace WebApplication2.Client
{
    public class RestaurantContactClient
    {
        private HttpClient _httpClient;
        private static string _address;

        public RestaurantContactClient()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<Contacts> RestaurantsContactsAsync(string placeid)
        {

            var response = await _httpClient.GetAsync($"/RestaurantContacts?PlaceID={placeid}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<Contacts>(content);
            return responseObject;

        }
    }
}
