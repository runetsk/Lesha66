using Microsoft.Playwright;
using OpenQA.Selenium.Chrome;

namespace Lesha66;

public class Utils
{
    public ChromeDriver GetDriverSeleniumMaximized()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        var driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        return driver;
    }
    
    public ChromeDriver GetDriverSelenium()
    {
        var options = new ChromeOptions();
        return new ChromeDriver(options);
    }
    
    public async Task<IBrowser> GetBrowserContextPlaywrightAsync()
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });
        return browser;
    }

    public async Task<(IBrowser browser, IBrowserContext context)> GetBrowserContextPlaywrightMaximizedAsync()
    {
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = new List<string> { "--start-maximized" }
        };
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(launchOptions);
        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });
        return (browser, context);
    }
}