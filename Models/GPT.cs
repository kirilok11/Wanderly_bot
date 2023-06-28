using Newtonsoft.Json;
using System;

namespace MvcMovie.Models
{


    public class Usagei
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }

    public class Messagei
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class Choicei
    {
        public Messagei message { get; set; }
        public string finish_reason { get; set; }
        public int index { get; set; }
    }

    public class GPTResponse
    {
        public string id { get; set; }
        public string @object { get; set; }
        public long created { get; set; }
        public string model { get; set; }
        public Usagei usage { get; set; }
        public List<Choicei> choices { get; set; }
    }
}

