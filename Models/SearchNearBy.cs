using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace MvcMovie.Models
{
    public class Geometry1
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
        public string xid { get; set; }
        public int rate { get; set; }
        public string name { get; set; }
        public string osm { get; set; }
        public double dist { get; set; }
        public string kinds { get; set; }
        public string wikidata { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public int id { get; set; }
        public Geometry1 geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Root1
    {
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }
}
