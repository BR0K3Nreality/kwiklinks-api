using UrlShortener.Models;
using System.Threading.Tasks;

namespace UrlShortener.Services
{
    public interface IShortUrlService
    {
        Task<string> ShortenUrl(string originalUrl, int expiryInDays);
        Task<string?> GetOriginalUrl(string shortenedUrl);
    }
}
