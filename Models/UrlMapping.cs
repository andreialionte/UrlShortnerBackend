// Models/UrlMapping.cs
using System.ComponentModel.DataAnnotations;

public class UrlMapping
{
    [Key]
    public int Id { get; set; }

    public string? OriginalUrl { get; set; }

    public string? ShortUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ClickCount { get; set; } = 0;
}
