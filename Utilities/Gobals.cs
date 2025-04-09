namespace AdPlatformsTestWork.Utilities;

public static class Globals
{
    private static bool IsProduction =>
        bool.TryParse(Environment.GetEnvironmentVariable("IS_PRODUCTION"), out var result) && result;
    
    
    public static string CACHE_SERVER_IP => IsProduction ? Environment.GetEnvironmentVariable("CACHE_SERVER_IP") ?? "localhost" : "localhost";
    public static string REDIS_PORT => IsProduction ? Environment.GetEnvironmentVariable("REDIS_PORT") ?? "6379" : "6379";
}
