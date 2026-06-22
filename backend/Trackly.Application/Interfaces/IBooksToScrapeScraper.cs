using Trackly.Application.Models;
namespace Trackly.Application.Interfaces;
public interface IBooksToScrapeScraper { Task<IReadOnlyList<ScrapedProduct>> ScrapeAsync(string baseUrl, CancellationToken cancellationToken = default); }
