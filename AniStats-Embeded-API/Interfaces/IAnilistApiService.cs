using AniStats_Embeded_API.Models;

namespace AniStats_Embeded_API.Interfaces;

public interface IAnilistApiService
{
    Task<AnilistApiUserResult> GetUserDataForApi(string username);
    Task<AnilistUserStatsResponseDto?> GetUserData(string username);
}