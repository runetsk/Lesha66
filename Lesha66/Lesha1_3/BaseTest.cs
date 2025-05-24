namespace Lesha66;

public class BaseTest
{
    public string BaseUrl { get; set; } = "https://www.a1qa.com/";
    public int ParallelTestCount { get; set; } = 5;
    public string TestUserName { get; set; } = "Lesha Ermolinski";
    public string TestCompany { get; set; } = "NonA1QA";
    public string TestMessage { get; set; } = "Jal' vas bedolagi";

    protected readonly Utils Utils = new();
}