namespace MvcMovie.Models
{
   



    public class LocationData
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }



    public class GeometryData
    {

        public LocationData location { get; set; }


        public class ResultData
        {


            public GeometryData geometry { get; set; }
            public string place_id { get; set; }

        }

        public class GeocodingResponse
        {
            public List<ResultData> results { get; set; }

        }
    }

}