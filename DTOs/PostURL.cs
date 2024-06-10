using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DTOs
{
    public class PostURL
    {
        [Required(ErrorMessage = "URL Required")]
        public string originalUrl { get; set; }
        public int ExpiryInDays { get; set; }
    }
}