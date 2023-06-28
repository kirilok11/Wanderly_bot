namespace MvcMovie.Models
{
    
    public class WikiResponse
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public List<string> Summary { get; set; }
        public List<SimilarItem> Similar { get; set; }
    }

    public class SimilarItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}


