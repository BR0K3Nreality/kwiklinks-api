using Dapper;
using UrlShortener.Models;
using System.Data;

namespace UrlShortener.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly IDbConnection _dbConnection;

        public ShortUrlRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ShortUrl?> GetByShortenedUrl(string shortenedUrl)
        {
            var sql = "SELECT * FROM ShortUrls WHERE \"shortenedUrl\" = @su";
            return await _dbConnection.QuerySingleOrDefaultAsync<ShortUrl>(sql, new { su = shortenedUrl });
        }

        public async Task Add(ShortUrl shortUrl)
        {
            var sql = "INSERT INTO ShortUrls (\"originalUrl\", \"shortenedUrl\", \"createdAt\", \"expiryDate\") VALUES (@originalUrl, @shortenedUrl, @createdAt, @expiryDate)";
            await _dbConnection.ExecuteAsync(sql, shortUrl);
        }
    }
}
