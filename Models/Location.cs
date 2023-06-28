using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;


using System;
using Newtonsoft.Json;

public class AddressComponent
{
    public string LongName { get; set; }
    public string ShortName { get; set; }
    public string[] Types { get; set; }
}

public class PlusCode
{
    [JsonProperty("compoundCode")]
    public string CompoundCode { get; set; }

    [JsonProperty("global_code")]
    public string GlobalCode { get; set; }
}

public class Location
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}

public class Northeast
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}

public class Southwest
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}

public class Viewport
{
    public Northeast Northeast { get; set; }
    public Southwest Southwest { get; set; }
}

public class Geometry
{
    public Location Location { get; set; }

    [JsonProperty("location_type")]
    public string LocationType { get; set; }

    public Viewport Viewport { get; set; }
}

public class Result
{
    [JsonProperty("address_components")]
    public AddressComponent[] AddressComponents { get; set; }

    [JsonProperty("formattedAddress")]
    public string FormattedAddress { get; set; }

    public Geometry Geometry { get; set; }

    [JsonProperty("place_id")]
    public string PlaceId { get; set; }

    [JsonProperty("plus_code")]
    public PlusCode PlusCode { get; set; }

    public string[] Types { get; set; }
}

public class Root
{
    [JsonProperty("plusCode")]
    public PlusCode PlusCode { get; set; }

    public Result[] Results { get; set; }
}