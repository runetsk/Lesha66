using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace Lesha66;

[SimpleJob(RunStrategy.ColdStart, launchCount: 2, iterationCount: 4)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class FillFormSingleThreadTests : FillFormTest
{
    [Benchmark]
    public async Task FillForm_Selenium_Xpath_NonHeadless_Maximized()
    {
        var driver = Utils.GetDriverSeleniumMaximized();
        await FillForm_Selenium_Xpath(driver);
    }

    [Benchmark]
    public async Task FillForm_Playwright_XPath_NonHeadless_Maximized()
    {
        var (browser, context) = await Utils.GetBrowserContextPlaywrightMaximizedAsync();
        await FillForm_Playwright_XPath(browser, context);
    }
    
    [Benchmark]
    public async Task FillForm_Playwright_NonXpath_NonHeadless_Maximized()
    {
        var (browser, context) = await Utils.GetBrowserContextPlaywrightMaximizedAsync();
        await FillForm_Playwright_NonXpath(browser, context);
    }
}