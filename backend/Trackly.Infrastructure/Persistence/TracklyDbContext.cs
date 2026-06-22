using Microsoft.EntityFrameworkCore;
using Trackly.Domain.Entities;
namespace Trackly.Infrastructure.Persistence;
public class TracklyDbContext(DbContextOptions<TracklyDbContext> options) : DbContext(options) {
 public DbSet<Product> Products => Set<Product>(); public DbSet<PriceHistory> PriceHistories => Set<PriceHistory>(); public DbSet<Source> Sources => Set<Source>(); public DbSet<ScrapeJob> ScrapeJobs => Set<ScrapeJob>();
 protected override void OnModelCreating(ModelBuilder b) { b.Entity<Product>().HasIndex(x => new { x.SourceId, x.ProductUrl }).IsUnique(); b.Entity<Product>().Property(x => x.CurrentPrice).HasPrecision(12,2); b.Entity<PriceHistory>().Property(x => x.Price).HasPrecision(12,2); b.Entity<Source>().HasData(new Source { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name="Books to Scrape", BaseUrl="https://books.toscrape.com", SourceType="Product", IsActive=true, CreatedAt=new DateTime(2024,1,1,0,0,0,DateTimeKind.Utc) }); }
}
