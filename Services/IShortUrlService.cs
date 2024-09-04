namespace UrlShortner.Services
{
    public interface IShortUrlService
    {
        Task<string> ShortingTheUrl(string url);
        Task<string> GetUrl(string url);
    }
}
