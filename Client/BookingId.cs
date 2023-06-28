using MvcMovie.Models;
using Newtonsoft.Json;
using static MvcMovie.Models.GeometryData;

namespace WebApplication2.Client
{
    public class BookingId
    {
        private HttpClient _httpClient;
        private static string _address;

        public BookingId()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<List<CityData>> BookingIDAsync(string text)
        {
            string restext = "";
            for (int c = 0; c < text.Length; c++)
            {
                if (text[c] == ' ')
                {
                    restext = restext + "%2C%20";
                }
                else
                {
                    restext = restext + text[c];
                }

            }
            var response = await _httpClient.GetAsync($"/PlaceID?place={restext}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<List<CityData>>(content);
            return responseObject;

        }
    }
}
