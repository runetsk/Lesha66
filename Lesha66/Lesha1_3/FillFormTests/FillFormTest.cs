using Microsoft.Playwright;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Lesha66;

public class FillFormTest : BaseTest
{
    public async Task<string> FillForm_Selenium_Xpath(ChromeDriver driver)
    {
        try
        {
            await driver.Navigate().GoToUrlAsync(BaseUrl);
            driver.FindElement(By.XPath("//div[@class='header__contact']//a")).Click();
            driver.FindElement(By.XPath("//*[@id='your-name']")).SendKeys(TestUserName);
            driver.FindElement(By.XPath("//*[@id='your-company']")).SendKeys(TestCompany);
            driver.FindElement(By.XPath("//*[@id='your-message']")).SendKeys(TestMessage);
            driver.FindElement(By.XPath("//span[@class='checkbox__item']")).Click();

            driver.FindElement(By.XPath("//nav[@class='menu']//a[text()='Blog']")).Click();
            driver.FindElement(By.XPath("//div[@class='blogList']//a[@class='blogCard ']")).Click();
            return driver.FindElement(By.XPath("//div[@class='blogPost']")).Text;
        }
        finally
        {
            driver?.Quit();
        }
    }

    public async Task<string?> FillForm_Playwright_XPath(IBrowser browser, IBrowserContext context)
    {
        try
        {
            var page = await context.NewPageAsync();
            await page.GotoAsync(BaseUrl);
            await page.ClickAsync("xpath=//div[@class='header__contact']//a");
            await page.FillAsync("xpath=//*[@id='your-name']", TestUserName);
            await page.FillAsync("xpath=//*[@id='your-company']", TestCompany);
            await page.FillAsync("xpath=//*[@id='your-message']", TestMessage);
            await page.ClickAsync("xpath=//span[@class='checkbox__item']");

            await page.ClickAsync("xpath=//nav[@class='menu']//a[text()='Blog']");
            await page.ClickAsync("xpath=//div[@class='blogList']//a[@class='blogCard ']");
            return await page.TextContentAsync("xpath=//div[@class='blogPost']");
        }
        finally
        {
            await context.CloseAsync();
            await browser.CloseAsync();
        }
    }

    public async Task FillForm_Playwright_NonXpath(IBrowser browser, IBrowserContext context)
    {
        try
        {
            var page = await context.NewPageAsync();
            await page.GotoAsync(BaseUrl);
            await page.GetByRole(AriaRole.Link, new() { Name = "Contact Us" }).ClickAsync();
            await page.Locator("#your-name").FillAsync(TestUserName);
            await page.Locator("#your-company").FillAsync(TestCompany);
            await page.Locator("#your-message").FillAsync(TestMessage);
            await page.Locator(".contactsForm__row .checkbox").First.ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Blog" }).ClickAsync();
            await page.Locator("a.blogCard").First.ClickAsync();
            var text = await page.Locator("div.blogPost").TextContentAsync();
        }
        finally
        {
            await context.CloseAsync();
            await browser.CloseAsync();
        }
    }
}