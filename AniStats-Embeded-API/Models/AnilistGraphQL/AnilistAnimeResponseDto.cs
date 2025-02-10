namespace AniStats_Embeded_API.Models;

public class AnilistAnimeResponseDto
{
    public Page Page { get; set; }
}
public class Page
{
    public List<Media> Media { get; set; }
}
public class Media
{
    public int Id { get; set; }
    public Title Title { get; set; }
    public CoverImage CoverImage { get; set; }
    public int Popularity { get; set; }
}
public class Title
{
    public string English { get; set; }
}
public class CoverImage
{
    public string ExtraLarge { get; set; }
}

