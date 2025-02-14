using System.Xml.Linq;
using AniStats_Embeded_API.Helpers.Extensions;
using AniStats_Embeded_API.Interfaces;
using AniStats_Embeded_API.Models;
using AniStats_Embeded_API.Models.ProfileCustomisations;
using Microsoft.Playwright;

namespace AniStats_Embeded_API.Helpers;
public class HtmlToStringHelper : IHtmlToStringHelper
{
    private readonly ILogger<HtmlToStringHelper> _logger;
    private readonly IPlaywrightService _playwrightService;
    private readonly IStatsCalculatorHelper _statsCalculatorHelper;
    
    public HtmlToStringHelper(ILogger<HtmlToStringHelper> logger, IPlaywrightService playwrightService, IStatsCalculatorHelper statsCalculatorHelper)
    {
        _logger = logger;
        _playwrightService = playwrightService;
        _statsCalculatorHelper = statsCalculatorHelper;
    }
    
    public async Task<string> RenderHtmlToString(Templates templateType, AnilistUserStatsResponseDto userStats)
    {
        try
        {
            var templatePath = templateType.GetTemplateFilePath();
            var htmlContent = await File.ReadAllTextAsync(templatePath);
            var populatedHtml = await PopulateHtmlValues(htmlContent, userStats);
            return populatedHtml;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error rendering HTML to string");
            return "";
        }
    }
    
    

    private async Task<string> PopulateHtmlValues(string htmlContent, AnilistUserStatsResponseDto userStats)
    {
        htmlContent = htmlContent.Replace("{Username}", userStats.User.Name);
        htmlContent = htmlContent.Replace("{AvatarUrl}", userStats.User.Avatar.Large);
        htmlContent = htmlContent.Replace("{BannerUrl}", userStats.User.BannerImage);


        var animeStatRange = await _statsCalculatorHelper.CalculateAnimeStatRange((int)userStats.User.Statistics.Anime.DaysWatched);
        htmlContent = htmlContent.Replace("{TotalAnime}", userStats.User.Statistics.Anime.Count.ToString());
        htmlContent = htmlContent.Replace("{TotalDaysWatched}", userStats.User.Statistics.Anime.DaysWatched.ToString());
        htmlContent = htmlContent.Replace("{AnimeMeanScore}", userStats.User.Statistics.Anime.MeanScore.ToString());
        htmlContent = htmlContent.Replace("{AnimeDaysWatchedLow}", animeStatRange.Low.ToString());
        htmlContent = htmlContent.Replace("{AnimeDaysWatchedMid}", animeStatRange.Mid.ToString());
        htmlContent = htmlContent.Replace("{AnimeDaysWatchedHigh}", animeStatRange.High.ToString());
        
        var mangaStateRange = await _statsCalculatorHelper.CalculateMangaStatRange(userStats.User.Statistics.Manga.ChaptersRead);
        htmlContent = htmlContent.Replace("{TotalManga}", userStats.User.Statistics.Manga.Count.ToString());
        htmlContent = htmlContent.Replace("{TotalChaptersRead}", userStats.User.Statistics.Manga.ChaptersRead.ToString());
        htmlContent = htmlContent.Replace("{MangaMeanScore}", userStats.User.Statistics.Manga.MeanScore.ToString());
        htmlContent = htmlContent.Replace("{MangaChaptersReadLow}", mangaStateRange.Low.ToString());
        htmlContent = htmlContent.Replace("{MangaChaptersReadMid}", mangaStateRange.Mid.ToString());
        htmlContent = htmlContent.Replace("{MangaChaptersReadHigh}", mangaStateRange.High.ToString());

        var animeProgressPercentage = await _statsCalculatorHelper.CalculateProgressPercentage(animeStatRange, userStats.User.Statistics.Anime.DaysWatched);
        var mangaProgressPercentage = await _statsCalculatorHelper.CalculateProgressPercentage(mangaStateRange, userStats.User.Statistics.Manga.ChaptersRead);

        
        htmlContent = htmlContent.Replace("{AnimeProgressPercentage}", animeProgressPercentage.ToString());
        htmlContent = htmlContent.Replace("{MangaProgressPercentage}", mangaProgressPercentage.ToString());
        return htmlContent;
    }
    

}