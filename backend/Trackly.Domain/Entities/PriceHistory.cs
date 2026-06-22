namespace Trackly.Domain.Entities;
public class PriceHistory { public Guid Id { get; set; } = Guid.NewGuid(); public Guid ProductId { get; set; } public Product Product { get; set; } = null!; public decimal Price { get; set; } public string Currency { get; set; } = "GBP"; public DateTime ScrapedAt { get; set; } = DateTime.UtcNow; }
