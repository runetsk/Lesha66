# Tiny benchmark to compare Selenium and Playwright
## Big thanks to Alexey Ermolinski

## Browser Up And Navigate Benchmarks

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.5262/23H2/2023Update/SunValley3)
13th Gen Intel Core i5-13600K, 1 CPU, 20 logical and 14 physical cores
.NET SDK 9.0.100-rc.2.24474.11
[Host]     : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2
Job-RCPGSU : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2

IterationCount=10  LaunchCount=2  RunStrategy=ColdStart

```
| Method                                                   | Mean    | Error   | StdDev  | Min     | Max     | Median  |
|--------------------------------------------------------- |--------:|--------:|--------:|--------:|--------:|--------:|
| BrowserUpAndNavigate_Selenium_NonHeadless_Maximized      | 4.398 s | 1.371 s | 1.579 s | 1.532 s | 6.934 s | 4.158 s |
| BrowserUpAndNavigate_Playwright_NonHeadless_Maximized    | 4.372 s | 1.169 s | 1.346 s | 2.837 s | 9.104 s | 4.237 s |
| BrowserUpAndNavigate_Selenium_NonHeadless_NonMaximized   | 4.533 s | 1.503 s | 1.731 s | 1.516 s | 7.654 s | 4.153 s |
| BrowserUpAndNavigate_Playwright_NonHeadless_NonMaximized | 4.731 s | 1.070 s | 1.232 s | 3.040 s | 7.202 s | 4.539 s |



```
## Slightly more complex scenario with elements intercations


BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.5262/23H2/2023Update/SunValley3)
13th Gen Intel Core i5-13600K, 1 CPU, 20 logical and 14 physical cores
.NET SDK 9.0.100-rc.2.24474.11
[Host]     : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2
Job-VJKROT : .NET 9.0.0 (9.0.24.47305), X64 RyuJIT AVX2

IterationCount=20  LaunchCount=2  RunStrategy=ColdStart

```
| Method                                          | Mean    | Error    | StdDev   | Min     | Max     | Median  |
|------------------------------------------------ |--------:|---------:|---------:|--------:|--------:|--------:|
| FillForm_Selenium_Xpath_NonHeadless_Maximized   | 9.621 s | 0.5609 s | 0.9970 s | 7.827 s | 13.50 s | 9.326 s |
| FillForm_Playwright_XPath_NonHeadless_Maximized | 8.480 s | 0.7610 s | 1.3526 s | 6.782 s | 12.38 s | 8.018 s |
| FillForm_Playwright_NonXpath_NonHeadless        | 8.236 s | 0.6870 s | 1.2211 s | 5.585 s | 11.85 s | 7.882 s |

```
> **Author’s notes:**  
Time difference between Selenium and Playwright ~ 1 second (12%)
The observed differences are too small to claim a meaningful performance advantage for Playwright over Selenium. In real‑world end‑to‑end tests that spend most of their time waiting for backend calls and business‑logic processing, that micro‑difference is lost in the noise. You’ll still be waiting hundreds of milliseconds or several seconds on the server side.

> According to the results, XPath performs on par with `getByRole` and CSS selectors in Playwright. However, in practice CSS selectors and `getByRole` often produce more verbose, less intuitive code, forcing you to choose different locator strategies instead of applying a single consistent approach. This is the third time I've used playwright Non-Xpath locators so I’d love to see a PR from anyone more skilled than me.

### PRs to fix existing tests are always welcome as well as additional tests that will prove your point.
### Please make sure to run the benchmarks before and after your changes to ensure that performance is not degraded.


### TODO

- [ ] Add multi threading benchmarks for comparison
- [ ] Add headless browser benchmarks for comparison
- [ ] Test with additional browsers (Firefox, Safari, Edge)
- [ ] Create benchmarks for more complex interactions (drag & drop, scrolling, attachements)
- [ ] Add benchmarks for performance with large pages/DOM structures
- [ ] Compare memory usage between frameworks