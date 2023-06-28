using Newtonsoft.Json;

namespace MvcMovie.Models
{
    
    
       
    public class TranslationRequest
    {
        public string status { get; set; }
        public ApiDataY data { get; set; }
    }

    public class ApiDataY
    {
        public string translatedText { get; set; }
    }




}
