using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Location3
{
    public string lat { get; set; }
    public string lng { get; set; }
}




public class Geometry3
{
    public Location3 location { get; set; }
   
}





public class PlusCode3
{
    public string compound_code { get; set; }
    public string global_code { get; set; }
}

public class Result3
{
    public string business_status { get; set; }
    public Geometry3 geometry { get; set; }
   
    public string name { get; set; }
    public OpeningHours3 opening_hours { get; set; }
    
    public string place_id { get; set; }
    public PlusCode3 plus_code { get; set; }
    public int price_level { get; set; }
    public double rating { get; set; }
    public string reference { get; set; }
    public string scope { get; set; }
    public List<string> types { get; set; }
    public int user_ratings_total { get; set; }

   
    public string vicinity { get; set; }
}

public class OpeningHours3
{
    public bool open_now { get; set; }
}

public class Root3
{
    public List<object> html_attributions { get; set; }
    public List<Result3> results { get; set; }
}
