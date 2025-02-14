using System.Net;
using AniStats_Embeded_API.Interfaces;
using AniStats_Embeded_API.Models;
using AniStats_Embeded_API.Models.ProfileCustomisations;
using Microsoft.AspNetCore.Mvc;

namespace AniStats_Embeded_API.Controllers;

[ApiController]
[Route("api/v1/embedstats")]
public class EmbedStatsController : ControllerBase
{
    private readonly ILogger<EmbedStatsController> _logger;
    private readonly IAnilistApiService _anilistAPIService;
    private readonly IHtmlToStringHelper _htmlToStringHelper;
    private readonly IPlaywrightService _playwrightService;
    private readonly IRequestValidationHelpers _requestValidationHelpers;
    
    public EmbedStatsController(ILogger<EmbedStatsController> logger, IAnilistApiService anilistAPIService, 
        IHtmlToStringHelper htmlToStringHelper, 
        IPlaywrightService playwrightService,
        IRequestValidationHelpers requestValidationHelpers)
    {
        _logger = logger;
        _anilistAPIService = anilistAPIService;
        _htmlToStringHelper = htmlToStringHelper;
        _playwrightService = playwrightService;
        _requestValidationHelpers = requestValidationHelpers;
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
            return Ok(result.User.User.Name);
        }

        return StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    
    [HttpGet("embed", Name = "GetEmbedStatsPreview")]
    public async Task<IActionResult> GetUserProfileEmbed(string? username, int? template, string? profilecolour, string? theme, bool? showusername)
    {
        var validationResults = _requestValidationHelpers.ValidateApiRequest(username, template, profilecolour, theme, showusername);

        if (validationResults.StatusCode != HttpStatusCode.OK)
        {
            return StatusCode((int)validationResults.StatusCode, validationResults.ErrorMessage);
        }
        
        var request = _requestValidationHelpers.MapProfileOptionsRequest(username, template.Value, profilecolour, theme, showusername);
        var result = await _anilistAPIService.GetUserDataForApi(username);
        
        if (result.StatusCode == HttpStatusCode.OK)
        {
            var htmlContent = await _htmlToStringHelper.RenderHtmlToString(Templates.AnilistStyleProfileStats, result.User);
            // return Ok(htmlContent);
            try {
                var image = await _playwrightService.ConvertHtmlTemplateToImage(htmlContent);
                return File(image, "image/png");
            } catch (Exception e)
            {
                _logger.LogError(e, "Error generating image");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error generating image");
            }
            

        }
        
        return StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    
}