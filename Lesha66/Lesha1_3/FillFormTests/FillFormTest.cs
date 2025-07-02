using Microsoft.Playwright;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Lesha66;

public class FillFormTest : BaseTest
{
    private string BaseUrl { get; set; } = "https://store.steampowered.com/";

    public async Task<string> FillForm_Selenium_Xpath(ChromeDriver driver)
    {
        try
        {
            await driver.Navigate().GoToUrlAsync(BaseUrl);
            driver.FindElement(By.XPath("//div[@id='global_action_menu']//a[contains(@href,'login')]")).Click();
            driver.FindElement(By.XPath("(//div[@data-featuretarget='login']//input)[1]")).SendKeys(TestUserName);
            driver.FindElement(By.XPath("//div[@data-featuretarget='login']//input[@type='password']"))
                .SendKeys(TestPassword);
            driver.FindElement(By.XPath("//div[contains(@class,'tool-tip')]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            driver.FindElement(
                By.XPath("//div[@aria-label='Global Menu']//a[@data-tooltip-content='.submenu_Community']")).Click();
            var comments =
                driver.FindElements(By.XPath(
                    "//div[contains(@class,'UserReviewCardContent')]//div[contains(@class,'CardTextContent')]"));
            return comments[0].Text;
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
            await page.ClickAsync("xpath=//div[@id='global_action_menu']//a[contains(@href,'login')]");
            await page.FillAsync("xpath=(//div[@data-featuretarget='login']//input)[1]", TestUserName);
            await page.FillAsync("xpath=//div[@data-featuretarget='login']//input[@type='password']", TestPassword);
            await page.ClickAsync("xpath=//div[contains(@class,'tool-tip')]");
            await page.ClickAsync("xpath=//button[@type='submit']");

            await page.ClickAsync(
                "xpath=//div[@aria-label='Global Menu']//a[@data-tooltip-content='.submenu_Community']");
            var commentsLocator = page.Locator(
                "xpath=//div[contains(@class,'UserReviewCardContent')]//div[contains(@class,'CardTextContent')]");
        
            // Wait until there are at least 2 elements (index 1 exists)
            await commentsLocator.Nth(1).WaitForAsync(new LocatorWaitForOptions { Timeout = 5000 });
        
            var comments = await commentsLocator.AllAsync();
        
            // Now safely access the first element since we know there are at least 2
            return await comments[0].TextContentAsync();
        }
        finally
        {
            await context.CloseAsync();
            await browser.CloseAsync();
        }
    }

    public async Task<string?> FillForm_Playwright_NonXpath(IBrowser browser, IBrowserContext context)
    {
        try
        {
            var page = await context.NewPageAsync();
            await page.GotoAsync(BaseUrl);
            await page.Locator("#global_action_menu").GetByRole(AriaRole.Link).Filter(new() { HasText = "login" })
                .ClickAsync();
            await page.Locator("[data-featuretarget='login'] input").First.FillAsync(TestUserName);
            await page.Locator("[data-featuretarget='login'] input[type='password']").FillAsync(TestPassword);
            await page.ClickAsync("xpath=//div[contains(@class,'tool-tip')]");
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();

            await page.ClickAsync(
                "xpath=//div[@aria-label='Global Menu']//a[@data-tooltip-content='.submenu_Community']");
            var commentsLocator = page.Locator(
                "xpath=//div[contains(@class,'UserReviewCardContent')]//div[contains(@class,'CardTextContent')]");
        
            // Wait until there are at least 2 elements (index 1 exists)
            await commentsLocator.Nth(1).WaitForAsync(new LocatorWaitForOptions { Timeout = 5000 });
        
            var comments = await commentsLocator.AllAsync();
        
            // Now safely access the first element since we know there are at least 2
            return await comments[0].TextContentAsync();
        }
        finally
        {
            await context.CloseAsync();
            await browser.CloseAsync();
        }
    }
}