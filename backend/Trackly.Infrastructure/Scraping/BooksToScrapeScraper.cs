using System.Globalization;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Trackly.Application.Interfaces;
using Trackly.Application.Models;
namespace Trackly.Infrastructure.Scraping;
public class BooksToScrapeScraper(HttpClient http, IOptions<ScraperOptions> options) : IBooksToScrapeScraper {
 public async Task<IReadOnlyList<ScrapedProduct>> ScrapeAsync(string baseUrl, CancellationToken ct = default) {
  var result = new List<ScrapedProduct>();
  for (var page=1; page<=Math.Max(1, options.Value.MaxPages); page++) { if(page>1) await Task.Delay(options.Value.DelayMilliseconds, ct); var url=$"{baseUrl.TrimEnd('/')}/catalogue/page-{page}.html"; using var response=await http.GetAsync(url,ct); if(response.StatusCode==System.Net.HttpStatusCode.NotFound) break; response.EnsureSuccessStatusCode(); var doc=new HtmlDocument(); doc.LoadHtml(await response.Content.ReadAsStringAsync(ct)); var cards=doc.DocumentNode.SelectNodes("//article[contains(@class,'product_pod')]"); if(cards is null or { Count: 0 }) break;
   foreach(var c in cards) { var anchor=c.SelectSingleNode(".//h3/a"); var name=HtmlEntity.DeEntitize(anchor?.GetAttributeValue("title","") ?? "").Trim(); var rel=anchor?.GetAttributeValue("href","") ?? ""; var productUrl=new Uri(new Uri(url),rel).AbsoluteUri; var imageRel=c.SelectSingleNode(".//img")?.GetAttributeValue("src",""); var image=string.IsNullOrWhiteSpace(imageRel)?null:new Uri(new Uri(url),imageRel).AbsoluteUri; var raw=c.SelectSingleNode(".//*[contains(@class,'price_color')]")?.InnerText ?? "0"; decimal.TryParse(new string(raw.Where(ch=>char.IsDigit(ch)||ch=='.').ToArray()), NumberStyles.Number, CultureInfo.InvariantCulture, out var price); var availability=HtmlEntity.DeEntitize(c.SelectSingleNode(".//*[contains(@class,'availability')]")?.InnerText ?? "").Trim(); var rating=c.SelectSingleNode(".//p[contains(@class,'star-rating')]")?.GetAttributeValue("class","").Replace("star-rating","").Trim(); result.Add(new(name,price,"GBP",productUrl,image,null,availability,rating)); }
  } return result;
 }
}
