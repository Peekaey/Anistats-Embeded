using System.Net;
using AniStats_Embeded_API.Interfaces;
using AniStats_Embeded_API.Models;
using AniStats_Embeded_API.Models.ProfileCustomisations;

namespace AniStats_Embeded_API.Helpers;

public class RequestRequestValidationHelpers : IRequestValidationHelpers
{
    public ApiServiceResult ValidateApiRequest(string? username, int? template, string? profileColour, string? theme, bool? hideUsername)
    {
        if (username == null)
        {
            return new ApiServiceResult(HttpStatusCode.BadRequest, "Username is required");
        }
        
        if (template == null)
        {
            return new ApiServiceResult(HttpStatusCode.BadRequest, "Template selection is required");
        }
        
        if (!Enum.IsDefined(typeof(Templates), template))
        {
            return new ApiServiceResult(HttpStatusCode.BadRequest, "Invalid template selection");
        }
        
        return ApiServiceResult.AsSuccess();
    }

    public ApiProfileOptionsRequest MapProfileOptionsRequest(string username, int template, string? profileColour, string? theme, bool? hideUsername)
    {
        ApiProfileOptionsRequest mappedRequest = new ApiProfileOptionsRequest();
        mappedRequest.Username = username;
        mappedRequest.Template = (Templates)template;
        mappedRequest.ProfileColour = GetValidProfileColour(profileColour);
        mappedRequest.Theme = GetValidTheme(theme);
        mappedRequest.HideUsername = hideUsername ?? false;
        return mappedRequest;
    }


    
    private Theme GetValidTheme(string? theme)
    {
        // Default theme of dark
        if (theme == null)
        {
            return Theme.Dark;
        }
        var lowerCaseTheme = theme?.ToLower();
        var matchingTheme = Enum.GetNames(typeof(Theme))
            .FirstOrDefault(en => en.ToLower() == lowerCaseTheme);

        if (matchingTheme != null)
        {
            return Enum.Parse<Theme>(matchingTheme);
        }
        return Theme.Dark;
    }
    private ProfileColours GetValidProfileColour(string? profileColour)
    {
        // Default colour of blue
        if (profileColour == null)
        {
            return ProfileColours.Blue;
        }
        
        var lowerCaseProfileColour = profileColour?.ToLower();
        var matchingColour = Enum.GetNames(typeof(ProfileColours))
            .FirstOrDefault(en => en.ToLower() == lowerCaseProfileColour);

        if (matchingColour != null)
        {
            return Enum.Parse<ProfileColours>(matchingColour);
        }
        return ProfileColours.Blue;
    }
    
}