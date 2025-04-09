using System.Text;
using AdPlatformsTestWork.Controllers.Utilities;
using AdPlatformsTestWork.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace AdPlatformsTestWork.Controllers;

public class PlatformRepository
{
    private readonly IDistributedCache _cache;
    private readonly IDataNormalizer _dataNormalizer;
    
    private IDictionary<string, List<string>> _normalizePlatforms;
    
    public PlatformRepository()
    {
        //_cache = cache;
        _dataNormalizer = new JsonDataNormalizer();
    }

    public void Load(Platforms platforms)
    {
        _normalizePlatforms = _dataNormalizer.Normalize(platforms);
    }

    public JsonContent Search(string location)
    {
        var locationSegments = location.Trim('/').Split('/');
        var platforms = new List<string>();
        
        foreach (var segment in locationSegments)
        {
            var prefixLocation = new StringBuilder("/");
            prefixLocation.Append(segment);
            if (_normalizePlatforms.TryGetValue(prefixLocation.ToString(), out var locationPlatforms))
            {
                platforms.AddRange(locationPlatforms);
                
                return JsonContent.Create(platforms);
            }
            return JsonContent.Create("location is not found in cache");
        }
        
        return JsonContent.Create("location is empty");
    }
}