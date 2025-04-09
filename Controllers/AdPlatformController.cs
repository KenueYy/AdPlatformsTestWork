using AdPlatformsTestWork.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdPlatformsTestWork.Controllers;

[ApiController, Route("api/")]
public class AdPlatformController : ControllerBase
{
    private readonly PlatformRepository _repository;

    public AdPlatformController(PlatformRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("search")]
    public JsonResult GetLocation([FromQuery] string location)
    {
        if (!string.IsNullOrEmpty(location))
        {
            return new JsonResult(_repository.Search(location).Value);
        }
        
        return new JsonResult("location is empty");
     }
    
    [HttpPost("load")]
    public JsonResult Load([FromBody] Platforms platforms)
    {
        try
        {
            _repository.Load(platforms);
            Console.WriteLine("Data Loaded");
            return new JsonResult("Data Loaded");
        }
        catch (Exception e)
        {
            return new JsonResult($"Invalid Data: {e}");
        }
    }
}