using AniStats_Embeded_API.Models;
using AniStats_Embeded_API.Models.ProfileCustomisations;

namespace AniStats_Embeded_API.Helpers.Extensions;

public static class ProfileOptionsDictionaryExtension
{
    public static readonly Dictionary<ProfileColours, string> ProfileColourDictionary = new()
    {
        { ProfileColours.Blue, "#3db4f2" },
        { ProfileColours.Purple, "#c063ff" },
        { ProfileColours.Green, "#4cca51" },
        { ProfileColours.Orange, "#ef881a" },
        { ProfileColours.Red, "#e13333" },
        { ProfileColours.Pink, "fc9dd6" },
        { ProfileColours.Grey, "677b94" }
    };
    
    public static readonly Dictionary<Templates, string> TemplateDictionary = new()
    {
        { Templates.AnilistStyleProfileStats, "Templates/AnilistStyleProfileStats.html" }
    };

    public static string GetProfileColourHex(this ProfileColours profileColour)
    {
        return ProfileColourDictionary[profileColour];
    }
    
    public static string GetTemplateFilePath(this Templates template)
    {
        return TemplateDictionary[template];
    }


}