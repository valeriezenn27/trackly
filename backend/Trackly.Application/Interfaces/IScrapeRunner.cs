using Trackly.Domain.Entities;
namespace Trackly.Application.Interfaces;
public interface IScrapeRunner { Task<ScrapeJob> RunAsync(Guid sourceId, CancellationToken cancellationToken = default); }
