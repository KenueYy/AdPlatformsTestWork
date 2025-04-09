using AdPlatformsTestWork.Controllers;
using AdPlatformsTestWork.Utilities;

public class AdPlatformAPI
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();
        builder.Services.AddSingleton<PlatformRepository>();
        
        builder.Services.AddStackExchangeRedisCache(options => {
            options.Configuration = $"{Globals.CACHE_SERVER_IP}:{Globals.REDIS_PORT}";
        });
        
        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}