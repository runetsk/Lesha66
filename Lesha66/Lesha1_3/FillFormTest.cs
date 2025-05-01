using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Playwright;
using OpenQA.Selenium;

namespace Lesha66;

[SimpleJob(RunStrategy.ColdStart, launchCount: 2, iterationCount: 20)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class FillFormTest
{
    private readonly Utils _utils = new();

    [Benchmark]
    public async Task FillForm_Selenium_Xpath_NonHeadless_Maximized()
    {
        var driver = _utils.GetDriverSeleniumMaximized();
        try
        {
            await driver.Navigate().GoToUrlAsync("https://www.a1qa.com/");
            driver.FindElement(By.XPath("//div[@class='header__contact']//a")).Click();
            driver.FindElement(By.XPath("//*[@id='your-name']")).SendKeys("Lesha Ermolinski");
            driver.FindElement(By.XPath("//*[@id='your-company']")).SendKeys("NonA1QA");
            driver.FindElement(By.XPath("//*[@id='your-message']")).SendKeys("Jal' vas bedolagi");
            driver.FindElement(By.XPath("//span[@class='checkbox__item']")).Click();

            driver.FindElement(By.XPath("//nav[@class='menu']//a[text()='Blog']")).Click();
            driver.FindElement(By.XPath("//div[@class='blogList']//a[@class='blogCard ']")).Click();
            var text = driver.FindElement(By.XPath("//div[@class='blogPost']")).Text;
        }
        finally
        {
            driver.Quit();
        }
    }

    [Benchmark]
    public async Task FillForm_Playwright_XPath_NonHeadless_Maximized()
    {
        var context = await _utils.GetBrowserContextPlaywrightMaximizedAsync();
        try
        {
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://www.a1qa.com/");
            await page.ClickAsync("xpath=//div[@class='header__contact']//a");
            await page.FillAsync("xpath=//*[@id='your-name']", "Lesha Ermolinski");
            await page.FillAsync("xpath=//*[@id='your-company']", "NonA1QA");
            await page.FillAsync("xpath=//*[@id='your-message']", "Jal' vas bedolagi");
            await page.ClickAsync("xpath=//span[@class='checkbox__item']");

            await page.ClickAsync("xpath=//nav[@class='menu']//a[text()='Blog']");
            await page.ClickAsync("xpath=//div[@class='blogList']//a[@class='blogCard ']");
            var text = await page.TextContentAsync("xpath=//div[@class='blogPost']");
        }
        finally
        {
            await context.CloseAsync();
        }
    }

    [Benchmark]
    public async Task FillForm_Playwright_NonXpath_NonHeadless()
    {
        var context = await _utils.GetBrowserContextPlaywrightMaximizedAsync();
        try
        {
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://www.a1qa.com/");
            await page.GetByRole(AriaRole.Link, new() { Name = "Contact Us" }).ClickAsync();
            await page.Locator("#your-name").FillAsync("Lesha Ermolinski");
            await page.Locator("#your-company").FillAsync("NonA1QA");
            await page.Locator("#your-message").FillAsync("Jal' vas bedolagi");
            await page.Locator(".contactsForm__row .checkbox").First.ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Blog" }).ClickAsync();
            await page.Locator("a.blogCard").First.ClickAsync();
            var text = await page.Locator("div.blogPost").TextContentAsync();
        }
        finally
        {
            await context.CloseAsync();
        }
    }
}