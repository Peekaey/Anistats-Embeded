using AniStats_Embeded_API.Models;

namespace AniStats_Embeded_API.Interfaces;

public interface IRequestValidationHelpers
{
    ApiServiceResult ValidateApiRequest(string? username, int? template, string? profileColour, string? theme, bool? hideUsername);
    ApiProfileOptionsRequest MapProfileOptionsRequest(string username, int template, string? profileColour, string? theme, bool? hideUsername);
}