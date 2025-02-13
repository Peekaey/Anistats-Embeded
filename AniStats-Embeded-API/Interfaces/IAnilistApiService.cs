﻿using AniStats_Embeded_API.Models;

namespace AniStats_Embeded_API.Interfaces;

public interface IAnilistApiService
{
    Task<ApiServiceResult> GetUserDataForApi(string username);
    Task<AnilistUserResponseDto?> GetUserData(string username);
}