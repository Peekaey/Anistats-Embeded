using AniStats_Embeded_API.Models;

namespace AniStats_Embeded_API.Interfaces;

public interface IStatsCalculatorHelper
{
    Task<StatRange> CalculateAnimeStatRange(int totalAnime);
    Task<StatRange> CalculateMangaStatRange(int chaptersRead);
    Task<double> CalculateProgressPercentage(StatRange statRange, double statisticValue);
}