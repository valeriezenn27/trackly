using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Trackly.Application.Interfaces;
using Trackly.Infrastructure.Persistence;
using Trackly.Infrastructure.Scraping;
using Quartz;
namespace Trackly.Infrastructure;
public static class DependencyInjection { public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config) { services.AddDbContext<TracklyDbContext>(o=>o.UseNpgsql(config.GetConnectionString("TracklyDatabase"))); services.Configure<ScraperOptions>(config.GetSection("Scraper")); services.AddHttpClient<IBooksToScrapeScraper,BooksToScrapeScraper>((sp,http)=>{ var o=sp.GetRequiredService<IOptions<ScraperOptions>>().Value; http.DefaultRequestHeaders.UserAgent.ParseAdd(o.UserAgent); http.Timeout=TimeSpan.FromSeconds(30); }); services.AddScoped<IScrapeRunner,ScrapeRunner>(); services.AddQuartz(q=>{ var key=new JobKey("scheduled-scrape"); q.AddJob<ScheduledScrapeJob>(o=>o.WithIdentity(key)); q.AddTrigger(t=>t.ForJob(key).WithIdentity("scheduled-scrape-trigger").WithCronSchedule(config["Scraper:Schedule"] ?? "0 0 0/6 * * ?").StartNow()); }); services.AddQuartzHostedService(o=>o.WaitForJobsToComplete=true); return services; } }
