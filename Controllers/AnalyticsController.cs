using Microsoft.AspNetCore.Mvc;

namespace PersonalFinances.Controllers;

public class AnalyticsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}