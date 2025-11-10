using Microsoft.AspNetCore.Mvc;

namespace NoteSalesWeb.Controllers;

public class SeoController : Controller
{
    [HttpGet("sitemap.xml")]
    public IActionResult Sitemap()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var sitemap = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">
  <url>
    <loc>{baseUrl}/</loc>
    <lastmod>{DateTime.UtcNow:yyyy-MM-dd}</lastmod>
    <changefreq>weekly</changefreq>
    <priority>1.0</priority>
  </url>
  <url>
    <loc>{baseUrl}/Home/Privacy</loc>
    <lastmod>{DateTime.UtcNow:yyyy-MM-dd}</lastmod>
    <changefreq>monthly</changefreq>
    <priority>0.8</priority>
  </url>
  <url>
    <loc>{baseUrl}/Home/Terms</loc>
    <lastmod>{DateTime.UtcNow:yyyy-MM-dd}</lastmod>
    <changefreq>monthly</changefreq>
    <priority>0.8</priority>
  </url>
  <url>
    <loc>{baseUrl}/Home/ReturnPolicy</loc>
    <lastmod>{DateTime.UtcNow:yyyy-MM-dd}</lastmod>
    <changefreq>monthly</changefreq>
    <priority>0.8</priority>
  </url>
</urlset>";

        return Content(sitemap, "application/xml");
    }

    [HttpGet("robots.txt")]
    public IActionResult Robots()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var robots = $@"User-agent: *
Allow: /
Disallow: /api/
Disallow: /thank-you/

Sitemap: {baseUrl}/sitemap.xml";

        return Content(robots, "text/plain");
    }
}

