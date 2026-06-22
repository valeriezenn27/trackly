using Microsoft.EntityFrameworkCore;using Quartz;using Trackly.Application.Interfaces;using Trackly.Infrastructure.Persistence;
namespace Trackly.Infrastructure.Scraping;
[DisallowConcurrentExecution] public class ScheduledScrapeJob(TracklyDbContext db,IScrapeRunner runner):IJob{public async Task Execute(IJobExecutionContext context){var ids=await db.Sources.AsNoTracking().Where(x=>x.IsActive).Select(x=>x.Id).ToListAsync(context.CancellationToken);foreach(var id in ids)await runner.RunAsync(id,context.CancellationToken);}}
