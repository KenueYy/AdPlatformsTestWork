using AdPlatformsTestWork.Controllers;
using AdPlatformsTestWork.Models;

namespace Tests;

public class PlatformRepositoryTests
{
    [Fact]
    public async void Search_Test1()
    {
        var repository = new PlatformRepository();

        var platforms = new Platforms
        {
            { "Яндекс.Директ", new List<string> { "/ru" } },
            { "Ревдинский рабочий", new List<string> { "/ru/svrd/revda", "/ru/svrd/pervik" } },
            { "Газета уральских москвичей", new List<string> { "/ru/msk", "/ru/permobl", "/ru/chelobl" } },
            { "Крутая реклама", new List<string> { "/ru/svrd" } },
        };

        await repository.Load(platforms);

        var result = await repository.Search("/ru");

        Assert.Contains("Яндекс.Директ", result);
    }
    
    [Fact]
    public async void Search_Test2()
    {
        var repository = new PlatformRepository();

        var platforms = new Platforms
        {
            { "Яндекс.Директ", new List<string> { "/ru" } },
            { "Ревдинский рабочий", new List<string> { "/ru/svrd/revda", "/ru/svrd/pervik" } },
            { "Газета уральских москвичей", new List<string> { "/ru/msk", "/ru/permobl", "/ru/chelobl" } },
            { "Крутая реклама", new List<string> { "/ru/svrd" } },
        };

        await repository.Load(platforms);

        var result = await repository.Search("/ru/msk");

        Assert.Contains("Газета уральских москвичей", result);
        Assert.Contains("Яндекс.Директ", result);
    }
}