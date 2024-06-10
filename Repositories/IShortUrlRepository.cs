using UrlShortener.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrlShortener.Repositories
{
    public interface IShortUrlRepository
    {
        Task<ShortUrl?> GetByShortenedUrl(string shortenedUrl);
        Task Add(ShortUrl shortUrl);
    }
}
