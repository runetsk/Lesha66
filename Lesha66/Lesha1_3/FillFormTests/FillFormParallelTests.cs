using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace Lesha66;

[SimpleJob(RunStrategy.ColdStart, launchCount: 1, iterationCount: 10)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class FillFormParallelTests : FillFormTest
{
    [Benchmark]
    public async Task FillForm_Selenium_Xpath_NonHeadless_Maximized_Parallel()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < ParallelTestCount; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                var driver = Utils.GetDriverSeleniumMaximized();
                var s = await FillForm_Selenium_Xpath(driver);
            }));
        }

        await Task.WhenAll(tasks);
    }

    [Benchmark]
    public async Task FillForm_Playwright_XPath_NonHeadless_Maximized_Parallel()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < ParallelTestCount; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                var (browser, context) = Utils.GetBrowserContextPlaywrightMaximizedAsync().Result;
                var s = await FillForm_Playwright_XPath(browser, context);
            }));
        }

        await Task.WhenAll(tasks);
    }
}