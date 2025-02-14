using AniStats_Embeded_API.Interfaces;
using AniStats_Embeded_API.Models;

namespace AniStats_Embeded_API.Helpers;

public class StatsCalculatorHelper : IStatsCalculatorHelper
{
    public ILogger<StatsCalculatorHelper> Logger { get; }
    
    public StatsCalculatorHelper(ILogger<StatsCalculatorHelper> logger)
    {
        Logger = logger;
    }

    public  async Task<StatRange> CalculateAnimeStatRange(int totalAnime)
    {
        StatRange statRange = new();
        switch (totalAnime)
        {
            case int n when n < 5:
                statRange.Low = 0;
                statRange.Mid = 3;
                statRange.High = 5;
                break;
            case int n when n is > 5 and < 10:
                statRange.Low = 1;
                statRange.Mid = 8;
                statRange.High = 10;
                break;
            case int n when n is > 10 and < 50:
                statRange.Low = 10;
                statRange.Mid = 30;
                statRange.High = 50;
                break;
            case int n when n is > 50 and < 100:
                statRange.Low = 50;
                statRange.Mid = 75;
                statRange.High = 100;
                break;
            case > 100:
                statRange = await CalculateNearest50(totalAnime);
                break;
        }

        return statRange;
    }

    public async Task<StatRange> CalculateMangaStatRange(int chaptersRead)
    {
        StatRange statRange = new();
        switch (chaptersRead)
        {
            case int n when n < 100:
                statRange.Low = 0;
                statRange.Mid = 50;
                statRange.High = 100;
                break;
            case int n when n is > 100 and < 1000:
                statRange.Low = 100;
                statRange.Mid = 500;
                statRange.High = 1000;
                break;
            case int n when n is > 1000 and < 5000:
                statRange.Low = 1000;
                statRange.Mid = 3000;
                statRange.High = 5000;
                break;
            case int n when n is > 5000 and < 10000:
                statRange.Low = 5000;
                statRange.Mid = 7500;
                statRange.High = 10000;
                break;
            case > 10000:
                statRange = await CalculateNearest500(chaptersRead);
                break;
        }
        return statRange;
    }

    public async Task<double> CalculateProgressPercentage(StatRange statRange, double statisticValue)
    {
        return Math.Round(((double)statisticValue - (double)statRange.Low) / ((double)statRange.High - (double)statRange.Low) * 100 , 2);
    }

    private async Task<StatRange> CalculateNearest500(int chaptersRead)
    {
        StatRange statRange = new StatRange();
        statRange.Low = (chaptersRead / 1000) * 1000;
        statRange.Mid = statRange.Low + 500;
        statRange.High = statRange.Low + 1000;
        return statRange;
    }

    private async Task<StatRange> CalculateNearest50(int daysWatched)
    {
        StatRange statRange = new StatRange();
        statRange.Low = (daysWatched / 100) * 100;
        statRange.Mid = statRange.Low + 50;
        statRange.High = statRange.Low + 100;
        return statRange;
    }
    
}