using Microsoft.AspNetCore.Mvc;

namespace AniStats_Embeded_API.Controllers;

public class EmbedStatsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}