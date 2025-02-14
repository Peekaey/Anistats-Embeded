namespace AniStats_Embeded_API.Interfaces;

public interface IPlaywrightService
{
    Task<byte[]?> ConvertHtmlTemplateToImage(string htmlContent);
}