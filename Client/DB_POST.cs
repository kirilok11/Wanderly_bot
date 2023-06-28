using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Client
{
    public class DB_POST
    {
        private HttpClient _httpClient;
        private static string _address;

        public DB_POST()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);
        }

        public async Task<HttpResponseMessage> PostAsync(FavoriteDishPost favoriteDish)
        {
            var json = JsonConvert.SerializeObject(favoriteDish);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Favorite/post", content);
            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
