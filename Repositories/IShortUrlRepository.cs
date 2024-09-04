namespace UrlShortner.Repositories
{
    public interface IShortUrlRepository
    {
        Task<string> ShortenUrl(string url);
        Task<string> RedirectTheShortUrl(string url);
    }
}
