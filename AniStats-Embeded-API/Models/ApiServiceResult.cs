using System.Net;

namespace AniStats_Embeded_API.Models;

public class ApiServiceResult
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public ApiServiceResult(HttpStatusCode statusCode, string? errorMessage = null)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }

    public static ApiServiceResult AsSuccess(string? data = null)
    {
        return new ApiServiceResult(HttpStatusCode.OK) { Data = data };
    }

    public static ApiServiceResult AsFailure(HttpStatusCode statusCode, string errorMessage)
    {
        return new ApiServiceResult(statusCode, errorMessage);
    }
}