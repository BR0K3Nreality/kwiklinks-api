using UrlShortener.Repositories;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _repository;

        public ShortUrlService(IShortUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ShortenUrl(string originalUrl, int expiryInDays)
        {
            var shortenedUrl = GenerateShortUrl();
            var shortUrl = new ShortUrl
            {
                originalUrl = originalUrl,
                shortenedUrl = shortenedUrl,
                createdAt = DateTime.UtcNow,
                expiryDate = DateTime.UtcNow.AddDays(expiryInDays)
            };

            await _repository.Add(shortUrl);
            return shortenedUrl;
        }

        public async Task<string?> GetOriginalUrl(string shortenedUrl)
        {
            var shortUrl = await _repository.GetByShortenedUrl(shortenedUrl);
            if (shortUrl == null || shortUrl.expiryDate < DateTime.UtcNow)
            {
                return null;
            }

            return shortUrl.originalUrl;
        }

        private string GenerateShortUrl()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
