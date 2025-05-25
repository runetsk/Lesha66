using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace Lesha66;

[SimpleJob(RunStrategy.ColdStart, launchCount: 1, iterationCount: 5)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
[MarkdownExporterAttribute.GitHub]
public class UpAndNavigateBaseParallelTests : UpAndNavigateBaseTest
{
    [Benchmark]
    public async Task BrowserUpAndNavigate_Playwright_NonHeadless_Maximized_Parallel()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < ParallelTestCount; i++)
        {
            tasks.Add(Task.Run(BrowserUpAndNavigate_Playwright_NonHeadless_Maximized));
        }

        await Task.WhenAll(tasks);
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Selenium_NonHeadless_Maximized_Parallel()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < ParallelTestCount; i++)
        {
            tasks.Add(Task.Run(BrowserUpAndNavigate_Selenium_NonHeadless_Maximized));
        }

        await Task.WhenAll(tasks);
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Playwright_NonHeadless_NonMaximized_Parallel()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < ParallelTestCount; i++)
        {
            tasks.Add(Task.Run(BrowserUpAndNavigate_Playwright_NonHeadless_NonMaximized));
        }

        await Task.WhenAll(tasks);
    }

    [Benchmark]
    public async Task BrowserUpAndNavigate_Selenium_NonHeadless_NonMaximized_Parallel()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < ParallelTestCount; i++)
        {
            tasks.Add(Task.Run(BrowserUpAndNavigate_Selenium_NonHeadless_NonMaximized));
        }

        await Task.WhenAll(tasks);
    }
}