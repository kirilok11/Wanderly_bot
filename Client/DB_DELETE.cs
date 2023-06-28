using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Client
{
    public class DB_DELETE
    {
        private HttpClient _httpClient;
        private static string _address;

        public DB_DELETE()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Favorite/delete{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}