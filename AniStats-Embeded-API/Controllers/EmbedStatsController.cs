using System.Net;
using AniStats_Embeded_API.Interfaces;
using AniStats_Embeded_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AniStats_Embeded_API.Controllers;

[ApiController]
[Route("api/v1/embedstats")]
public class EmbedStatsController : ControllerBase
{
    private readonly ILogger<EmbedStatsController> _logger;
    private readonly IAnilistApiService _anilistAPIService;
    
    public EmbedStatsController(ILogger<EmbedStatsController> logger, IAnilistApiService anilistAPIService)
    {
        _logger = logger;
        _anilistAPIService = anilistAPIService;
    }


    [HttpGet("userprofiledata", Name = "GetEmbedStats")]
    public async Task<IActionResult> GetUserProfileData(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("Username is required");
        }
        
        var result = await _anilistAPIService.GetUserDataForApi(username);

        
        if (result.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result.Data);
        }

        return StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    
    [HttpGet("embed", Name = "GetEmbedStatsPreview")]
    public async Task<IActionResult> GetUserProfileEmbed(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("Username is required");
        }

        var result = await _anilistAPIService.GetUserData(username);
        
        var htmlContent = GenerateEmbedHtml(result);
        return Content(htmlContent, "text/html");
    }
    
    private string GenerateEmbedHtml(AnilistUserResponseDto userData)
    {
        return $@"
        <div style='width: 300px; border: 1px solid #ccc; padding: 10px; font-family: Arial, sans-serif;'>
            <h3 style='margin: 0; padding-bottom: 5px; text-align: center;'>{userData.User.Name}'s Profile</h3>
            <img src='{userData.User.Avatar.Large}' alt='Avatar' style='display: block; margin: auto; width: 100px; height: 100px; border-radius: 50%;'>
            <p><strong>Anime Watched:</strong> {userData.User.Id}</p>

        </div>";
    }
}