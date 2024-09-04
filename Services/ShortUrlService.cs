using UrlShortner.Repositories;

namespace UrlShortner.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public ShortUrlService(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<string> ShortingTheUrl(string originalUrl)
        {
            return await _shortUrlRepository.ShortenUrl(originalUrl);
        }


        public async Task<string> GetUrl(string shortUrl)
        {
            return await _shortUrlRepository.RedirectTheShortUrl(shortUrl);
        }
    }
}


