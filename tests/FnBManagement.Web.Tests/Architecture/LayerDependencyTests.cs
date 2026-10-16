using System.Text.RegularExpressions;

namespace FnBManagement.Web.Tests.Architecture;

public class LayerDependencyTests
{
    [Theory]
    [InlineData("Domain", "Application|Infrastructure|Controllers|Contracts|Configuration|Options")]
    [InlineData("Application", "Infrastructure|Controllers|Contracts|Configuration|Options")]
    [InlineData("Controllers", "Infrastructure")]
    public void SourceLayer_DoesNotReferenceOuterLayers(string layer, string forbiddenLayers)
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "FnBManagement.sln")))
            directory = directory.Parent;
        Assert.NotNull(directory);
        var files = Directory.GetFiles(
            Path.Combine(directory.FullName, "src", "FnBManagement.Web", layer), "*.cs", SearchOption.AllDirectories);
        Assert.NotEmpty(files);

        foreach (var file in files)
        {
            var source = File.ReadAllText(file);
            Assert.False(Regex.IsMatch(source, @"\bFnBManagement\.Web\.(?:" + forbiddenLayers + @")\b"),
                $"{file} references a layer outside its boundary.");
            if (layer is "Domain" or "Application")
                Assert.False(Regex.IsMatch(source, @"\bMicrosoft\.(?:EntityFrameworkCore|AspNetCore)\b"),
                    $"{file} depends on EF Core or ASP.NET Core.");
        }
    }
}
