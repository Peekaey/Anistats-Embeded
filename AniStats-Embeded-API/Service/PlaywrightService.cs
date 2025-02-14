using AniStats_Embeded_API.Interfaces;
using Microsoft.Playwright;

namespace AniStats_Embeded_API.Service;

public class PlaywrightService : IPlaywrightService
{
    private readonly ILogger<PlaywrightService> _logger;
    
    public PlaywrightService(ILogger<PlaywrightService> logger)
    {
        _logger = logger;
    }
    public async Task<byte[]?> ConvertHtmlTemplateToImage(string htmlContent)
    {
        try
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });
            var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var screenshot = await page.ScreenshotAsync();
            await browser.CloseAsync();
            return screenshot;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error converting HTML to image");
            return null;
        }
    }
}