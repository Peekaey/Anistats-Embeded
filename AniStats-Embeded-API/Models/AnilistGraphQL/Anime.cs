namespace AniStats_Embeded_API.Models;

public class Anime
{
    public int Count { get; set; }
    public double MeanScore { get; set; }
    public int MinutesWatched { get; set; }
    public double DaysWatched
    {
        get
        {
            if (MinutesWatched != 0)
            {
                return Math.Round(MinutesWatched / 1440.0, 2);
            }
            return 0;
        }
    }
}