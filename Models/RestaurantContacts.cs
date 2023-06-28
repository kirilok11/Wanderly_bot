using Newtonsoft.Json;

namespace MvcMovie.Models
{
    public class InfoX
{
    public bool delivery { get; set; }
    
    public string formatted_phone_number { get; set; }

        [JsonProperty("url")]
    public string url { get; set; }
}

public class Contacts
{
    
    public InfoX result { get; set; }
    
}
}
