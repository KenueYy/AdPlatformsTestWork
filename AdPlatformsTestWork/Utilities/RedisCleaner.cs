using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace AdPlatformsTestWork.Utilities;

public class RedisCleaner
{
    private readonly IConnectionMultiplexer  _redis;
    
    public RedisCleaner(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }
    
    public async Task FlushAllAsync()
    {
        foreach (var endPoint in _redis.GetEndPoints())
        {
            var server = _redis.GetServer(endPoint);
            await server.FlushDatabaseAsync(); // или FlushAllDatabasesAsync()
        }
    }
}