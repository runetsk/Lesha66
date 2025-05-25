using BenchmarkDotNet.Attributes;

namespace Lesha66;

public class UpAndNavigateBaseTest : BaseTest
{
    private string Url { get; set; } = "https://store.steampowered.com/";

    private readonly Utils _utils = new();

    [Benchmark]
    public async Task BrowserUpAndNavigate_Selenium_NonHeadless_Maximized()
    {
        var driver = _utils.GetDriverSeleniumMaximized();
        await driver.Navigate().GoToUrlAsync(Url);
        driver.Quit();
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Playwright_NonHeadless_Maximized()
    {
        var (browser, context) = await _utils.GetBrowserContextPlaywrightMaximizedAsync();
        var page = await context.NewPageAsync();
        await page.GotoAsync(Url);
        await context.CloseAsync();
        await browser.CloseAsync();
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Selenium_NonHeadless_NonMaximized()
    {
        var driver = _utils.GetDriverSelenium();
        await driver.Navigate().GoToUrlAsync(Url);
        driver.Quit();
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Playwright_NonHeadless_NonMaximized()
    {
        var browser = await _utils.GetBrowserContextPlaywrightAsync();
        var page = await browser.NewPageAsync();
        await page.GotoAsync(Url);
        await page.CloseAsync();
        await browser.CloseAsync();   
    }
}