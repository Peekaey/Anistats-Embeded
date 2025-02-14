using System.Net;

namespace AniStats_Embeded_API.Models;

public class AnilistApiUserResult
{
    public HttpStatusCode StatusCode { get; set; }
    public AnilistUserStatsResponseDto User { get; set; }
    public string? ErrorMessage { get; set; }

    public AnilistApiUserResult(HttpStatusCode statusCode, string? errorMessage = null)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }

    public static AnilistApiUserResult AsSuccess(AnilistUserStatsResponseDto user)
    {
        return new AnilistApiUserResult(HttpStatusCode.OK) { User = user  };
    }

    public static AnilistApiUserResult AsFailure(HttpStatusCode statusCode, string errorMessage)
    {
        return new AnilistApiUserResult(statusCode, errorMessage);
    }
}