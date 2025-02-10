namespace AniStats_Embeded_API.Models;


public class UserResponseDto
{
    public User User { get; set; }
}
public class User
{
    public Avatar Avatar { get; set; }
    public string BannerImage { get; set; }
    public int CreatedAt { get; set; }
    public string Name { get; set; }
}
public class Avatar
{
    public string Large { get; set; }
}
