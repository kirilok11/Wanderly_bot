namespace WebApplication2.Models
{
    public class DBGETresponse
    {
        public List<ChatItem> ChatItems { get; set; }
    }
    public class ChatItem
    {
        public int id { get; set; }
        public int chatID { get; set; }
        public string name { get; set; }
        public int rating { get; set; }
    }
}
