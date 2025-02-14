using AniStats_Embeded_API.Models.ProfileCustomisations;

namespace AniStats_Embeded_API.Models;

public class ApiProfileOptionsRequest
{
    public string Username { get; set; }
    public bool HideUsername { get; set; }
    public ProfileColours ProfileColour { get; set; }
    public Theme Theme { get; set; }
    public Templates Template { get; set; }
}