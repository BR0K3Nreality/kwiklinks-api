namespace UrlShortener.Models
{
    public class ShortUrl
    {
        //public int id { get; set; }
        public required string originalUrl { get; set; }
        public required string shortenedUrl { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime expiryDate { get; set; }
    }
}
