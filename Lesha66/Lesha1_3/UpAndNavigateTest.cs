using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace Lesha66;

[SimpleJob(RunStrategy.ColdStart, launchCount: 2, iterationCount: 10)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
[MarkdownExporterAttribute.GitHub]
public class UpAndNavigateTest
{
    private readonly Utils _utils = new();

    [Benchmark]
    public async Task BrowserUpAndNavigate_Selenium_NonHeadless_Maximized()
    {
        var driver = _utils.GetDriverSeleniumMaximized();
        await driver.Navigate().GoToUrlAsync("https://www.a1qa.com/");
        driver.Quit();
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Playwright_NonHeadless_Maximized()
    {
        var (browser, context) = await _utils.GetBrowserContextPlaywrightMaximizedAsync();
        var page = await context.NewPageAsync();
        await page.GotoAsync("https://www.a1qa.com/");
        await context.CloseAsync();
        await browser.CloseAsync();
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Selenium_NonHeadless_NonMaximized()
    {
        var driver = _utils.GetDriverSelenium();
        await driver.Navigate().GoToUrlAsync("https://www.a1qa.com/");
        driver.Quit();
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Playwright_NonHeadless_NonMaximized()
    {
        var browser = await _utils.GetBrowserContextPlaywrightAsync();
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://www.a1qa.com/");
        await page.CloseAsync();
    }
}