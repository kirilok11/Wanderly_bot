using System;
using System.Collections.Generic;
namespace MvcMovie.Models
{
    public class ActiveFilter
    {
        public string name { get; set; }
        public bool active { get; set; }
        public object url { get; set; }
        public string description { get; set; }
    }

    public class BestThumbnail
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Author
    {
        public string name { get; set; }
        public string channelID { get; set; }
        public string url { get; set; }
        public BestThumbnail bestAvatar { get; set; }
        public List<Thumbnail> avatars { get; set; }
        public List<string> ownerBadges { get; set; }
        public bool verified { get; set; }
    }

    public class Item
    {
        public string type { get; set; }
        public string title { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public BestThumbnail bestThumbnail { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
        public bool isUpcoming { get; set; }
        public object upcoming { get; set; }
        public bool isLive { get; set; }
        public List<object> badges { get; set; }
        public Author author { get; set; }
        public object description { get; set; }
        public int views { get; set; }
        public string duration { get; set; }
        public string uploadedAt { get; set; }
    }

    public class ApiResponseYT
    {
        public string originalQuery { get; set; }
        public string correctedQuery { get; set; }
        public int results { get; set; }
        public List<ActiveFilter> activeFilters { get; set; }
        public List<object> refinements { get; set; }
        public List<Item> items { get; set; }
    }

    


}
