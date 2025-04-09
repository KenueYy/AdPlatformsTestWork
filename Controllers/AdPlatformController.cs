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
    public async Task<IActionResult> GetLocation([FromQuery] string location)
    {
        var result = await _repository.Search(location);

        if (result.Count == 0)
        {
            return NotFound("No locations found");
        }
        
        return Ok(result);
    }
    
    [HttpPost("load")]
    public async Task<IActionResult> Load([FromBody] Platforms platforms)
    {
        try
        {
            await _repository.Load(platforms);
            return Ok(new {message = "Data Loaded"});
        }
        catch (Exception e)
        {
            return BadRequest(new { message = $"Invalid Data: {e.Message}" });
        }
    }
}