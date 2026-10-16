using FnBManagement.Web.Application.Search;

namespace FnBManagement.Web.Tests.Services;

public class SearchTermParserTests
{
    [Theory]
    [InlineData(null, new string[0])]
    [InlineData(" \t\n ", new string[0])]
    [InlineData("  chicken\tburger\n", new[] { "chicken", "burger" })]
    [InlineData("chicken \"spicy sauce\"", new[] { "chicken", "spicy sauce" })]
    [InlineData("chicken “spicy sauce”", new[] { "chicken", "spicy sauce" })]
    [InlineData("\"spicy sauce\" chicken", new[] { "spicy sauce", "chicken" })]
    [InlineData("chicken \"spicy sauce", new[] { "chicken", "spicy sauce" })]
    [InlineData("\"\" chicken \"   \"", new[] { "chicken" })]
    [InlineData("chicken\"spicy sauce\"", new[] { "chicken", "spicy sauce" })]
    [InlineData("\"spicy  sauce\"", new[] { "spicy  sauce" })]
    public void Parse_PreservesQuotedPhrases(string? input, string[] expected)
    {
        Assert.Equal(expected, SearchTermParser.Parse(input));
    }
}
