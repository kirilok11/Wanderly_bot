
using Newtonsoft.Json;
using System.Net;
using WebApplication2.Models;
using static MvcMovie.Models.GeometryData;

namespace WebApplication2.Client
{

    public class DB_GET
    {
        private HttpClient _httpClient;
        private static string _address;

        public DB_GET()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<List<ChatItem>> GetAsync()
        {

            var response = await _httpClient.GetAsync($"/api/Favorite/get");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject <List<ChatItem>>(content);
            return responseObject;

        }
    }
}
