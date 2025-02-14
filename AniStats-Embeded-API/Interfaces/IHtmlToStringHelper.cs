using AniStats_Embeded_API.Models;
using AniStats_Embeded_API.Models.ProfileCustomisations;

namespace AniStats_Embeded_API.Interfaces;

public interface IHtmlToStringHelper
{
    Task<string> RenderHtmlToString(Templates templateType, AnilistUserStatsResponseDto userStats);
}