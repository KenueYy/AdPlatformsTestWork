using AdPlatformsTestWork.Controllers;
using AdPlatformsTestWork.Utilities;
using StackExchange.Redis;

public class AdPlatformAPI
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();
        
#if CACHE_ENABLED
        
        builder.Services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect("45.89.65.103:6379")
        );
        builder.Services.AddScoped<RedisCleaner>();
        builder.Services.AddStackExchangeRedisCache(options => {
            options.Configuration = $"45.89.65.103:6379";
            options.InstanceName = "location";
        });
        
        builder.Services.AddScoped<PlatformRepository>();
#else        
        builder.Services.AddSingleton<PlatformRepository>();
#endif
        
        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}