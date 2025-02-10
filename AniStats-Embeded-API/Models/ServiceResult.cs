using System.Net;

namespace AniStats_Embeded_API.Models;

public class ServiceResult
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public ServiceResult(HttpStatusCode statusCode, string? errorMessage = null)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }

    public static ServiceResult AsSuccess(string data)
    {
        return new ServiceResult(HttpStatusCode.OK) { Data = data };
    }

    public static ServiceResult AsFailure(HttpStatusCode statusCode, string errorMessage)
    {
        return new ServiceResult(statusCode, errorMessage);
    }
}