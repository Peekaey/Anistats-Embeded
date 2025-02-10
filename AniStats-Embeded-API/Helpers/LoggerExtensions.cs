namespace AniStats_Embeded_API.Helpers;

public static class LoggerExtensions
{
    public static void LogActionTraceStart(this ILogger logger, string actionName)
    {
        logger.LogInformation($"LogActionTraceStart Start of {actionName} at {DateTime.Now}");
    }
    
    public static void LogActionTraceEnd(this ILogger logger, string actionName)
    {
        logger.LogInformation($"LogActionTraceEnd End of {actionName} at {DateTime.Now}");
    }
    
    public static void LogUnhandledException(this ILogger logger, Exception e, string errorMessage)
    {
        logger.LogError($"LogUnhandledException {errorMessage} at {DateTime.Now}. Exception : {e}");
    }
}