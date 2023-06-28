using Newtonsoft.Json;

namespace MvcMovie.Models
{
    public class CityData
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("rtl")]
        public int Rtl { get; set; }

        [JsonProperty("city_ufi")]
        public object CityUfi { get; set; }

        [JsonProperty("destId")]
        public string DestId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("nr_hotels")]
        public int NumberOfHotels { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

       

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("lc")]
        public string LanguageCode { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("cc1")]
        public string CountryCode1 { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("destinationType")]
        public string DestinationType { get; set; }

        [JsonProperty("hotels")]
        public int Hotels { get; set; }
    }

    

    public class ApiResponsePlaceID
    {
        public List<CityData> Data { get; set; }
    }

   

    
}
