namespace AniStats_Embeded_API.Models;

public class User
{
    public Avatar Avatar { get; set; }
    public string BannerImage { get; set; }
    public int CreatedAt { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }
    public string SiteUrl { get; set; }
    public Statistics Statistics { get; set; }
}