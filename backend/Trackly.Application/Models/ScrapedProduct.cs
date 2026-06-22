namespace Trackly.Application.Models;
public record ScrapedProduct(string Name, decimal Price, string Currency, string ProductUrl, string? ImageUrl, string? Category, string? Availability, string? Rating);
