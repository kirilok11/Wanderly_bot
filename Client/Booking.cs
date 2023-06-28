using MvcMovie.Models;
using Newtonsoft.Json;

namespace WebApplication2.Client
{
    public class Booking
    {
        private HttpClient _httpClient;
        private static string _address;

        public Booking()
        {
            _address = Constants.address;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_address);

        }
        public async Task<RootBook> BookingHotelAsync(string CheckInDate, string DestType, string CheckOutDate, string NumOfAdults, string DestId, string NumOfRooms, string NumOfChildren, string AgeOfChildren)
        {
            string restext = "";
            for (int c = 0; c < AgeOfChildren.Length; c++)
            {
                if (AgeOfChildren[c] == ' ')
                {
                   
                }
                else if (AgeOfChildren[c] == '.')
                {
                    restext = restext + "%2C";
                    
                }
                else if (AgeOfChildren[c] == ',') 
                {
                    restext = restext + "%2C";
                }
                else
                {
                    restext = restext + AgeOfChildren[c];

                }

            }
            var response = await _httpClient.GetAsync($"/Booking?CheckInDate={CheckInDate}&DestType={DestType}&CheckOutDate={CheckOutDate}&NumOfAdults={NumOfAdults}&DestId={DestId}&NumOfRooms={NumOfRooms}&NumOfChildren={NumOfChildren}&AgeOfChildren={AgeOfChildren}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;

            var responseObject = JsonConvert.DeserializeObject<RootBook>(content);
            return responseObject;

        }
    }
}
