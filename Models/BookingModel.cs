using Newtonsoft.Json;

namespace MvcMovie.Models
{
    public class Hotel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("reviewScore")]
        public string ReviewScore { get; set; }

        [JsonProperty("minTotalPrice")]
        public decimal MinTotalPrice { get; set; }

      
        
        [JsonProperty("distanceFromCC")]
        public string DistanceFromCC { get; set; }
        [JsonProperty("isBreakfastIncluded")]
        public string IsBreakfastIncluded { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }

    }

    public class RootBook
    {
        [JsonProperty("hotels")]
        public List<Hotel> Hotels { get; set; }

        
    }
}
