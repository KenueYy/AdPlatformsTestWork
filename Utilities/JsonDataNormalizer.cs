using AdPlatformsTestWork.Models;

namespace AdPlatformsTestWork.Controllers.Utilities;

public interface IDataNormalizer
{
    public IDictionary<string, List<string>> Normalize(Platforms value);
}

public class JsonDataNormalizer : IDataNormalizer
{
    public IDictionary<string, List<string>> Normalize(Platforms platforms)
    {
        var normalizePlatforms = new Dictionary<string, List<string>>();
        foreach (var platform in platforms)
        {
            foreach (var location in platform.Value)
            {
                if (normalizePlatforms.TryGetValue(location, out var item))
                {
                    item.Add(platform.Key);
                    continue;
                }
                
                normalizePlatforms.Add(location, new List<string>{platform.Key});
            }    
        }

        return normalizePlatforms;
    }
}