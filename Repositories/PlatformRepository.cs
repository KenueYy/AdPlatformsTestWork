using System.Text;
using System.Text.Json;
using AdPlatformsTestWork.Controllers.Utilities;
using AdPlatformsTestWork.Models;
using AdPlatformsTestWork.Utilities;
using Microsoft.Extensions.Caching.Distributed;

namespace AdPlatformsTestWork.Controllers;

public class PlatformRepository
{
    private readonly IDataNormalizer _dataNormalizer;
    private readonly RedisCleaner _cleaner;
    
    private IDictionary<string, List<string>> _normalizePlatforms;
    
#if CACHE_ENABLED
    private readonly IDistributedCache _cache;
    public PlatformRepository(IDistributedCache cache, RedisCleaner cleaner)
    {
        _cache = cache;
        _cleaner = cleaner;
        _dataNormalizer = new JsonDataNormalizer();
    }
    
#else

    public PlatformRepository()
    {
        _dataNormalizer = new JsonDataNormalizer();
    }
    
#endif

    public async Task Load(Platforms platforms)
    {
        _normalizePlatforms = _dataNormalizer.Normalize(platforms);
#if CACHE_ENABLED
        await _cleaner.FlushAllAsync();
        foreach (var platform in _normalizePlatforms)
        {
            await SetInCache(platform.Key, JsonSerializer.Serialize(platform.Value));
        }
#endif
    }

    public async Task<List<string>> Search(string location)
    {
        var locationSegments = location.Trim('/').Split('/');
        var platforms = new List<string>();
        var prefixLocation = new StringBuilder();

        foreach (var segment in locationSegments)
        {
            prefixLocation.Append('/').Append(segment);
            var key = prefixLocation.ToString();

#if CACHE_ENABLED
            var cachedPlatforms = await GetInCache(key);
            if (cachedPlatforms.Count > 0)
            {
                platforms.AddRange(cachedPlatforms);
            }
#else
            if (_normalizePlatforms.TryGetValue(key, out var locationPlatforms))
            {
                platforms.AddRange(locationPlatforms);
            }
#endif
        }

        return platforms;
    }
    
#if CACHE_ENABLED

    private async Task SetInCache(string key, string value)
    {
        await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions());
    }

    private async Task<List<string>> GetInCache(string key)
    {
        var str = await _cache.GetStringAsync(key);
        if (string.IsNullOrEmpty(str))
        {
            return new List<string>();
        }

        var platforms = JsonSerializer.Deserialize<List<string>>(str);
        return platforms ?? new List<string>();
    }
    
#endif 
    
}