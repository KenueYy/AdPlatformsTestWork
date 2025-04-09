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
            ConnectionMultiplexer.Connect($"{Globals.CACHE_SERVER_IP}:{Globals.REDIS_PORT}")
        );
        builder.Services.AddScoped<RedisCleaner>();
        builder.Services.AddStackExchangeRedisCache(options => {
            options.Configuration = $"{Globals.CACHE_SERVER_IP}:{Globals.REDIS_PORT}";
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