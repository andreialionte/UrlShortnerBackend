using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;

namespace UrlShortner.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly DataContext _context;

        public ShortUrlRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> ShortenUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                throw new ArgumentException("Original URL cannot be empty.");
            }

            var existingUrl = await _context.urlMappings
                .FirstOrDefaultAsync(a => a.OriginalUrl == originalUrl);

            if (existingUrl != null)
            {
                return existingUrl.ShortUrl;
            }

            var newShortUrl = Guid.NewGuid().ToString().Substring(0, 8);
            var urlMapping = new UrlMapping
            {
                OriginalUrl = originalUrl,
                ShortUrl = newShortUrl,
                ClickCount = 0
            };

            await _context.urlMappings.AddAsync(urlMapping);
            await _context.SaveChangesAsync();

            return newShortUrl;
        }

        public async Task<string> RedirectTheShortUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
            {
                throw new ArgumentException("Short URL cannot be empty.", nameof(shortUrl));
            }

            // Log the actual value being queried
            Console.WriteLine($"Querying for shortUrl: '{shortUrl}'");

            var urlMapping = await _context.urlMappings
                .FirstOrDefaultAsync(a => a.ShortUrl == shortUrl);

            // Log the result of the query
            if (urlMapping == null)
            {
                Console.WriteLine("No URL mapping found.");
                return null;
            }

            Console.WriteLine($"Found URL mapping: OriginalUrl = '{urlMapping.OriginalUrl}'");
            return urlMapping.OriginalUrl;
        }


    }
}




