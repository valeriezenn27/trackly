namespace Trackly.Infrastructure.Scraping;
public class ScraperOptions { public int MaxPages { get; set; } = 3; public int DelayMilliseconds { get; set; } = 1000; public string UserAgent { get; set; } = "Trackly/1.0 (educational price tracker)"; }
