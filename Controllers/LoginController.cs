using Microsoft.AspNetCore.Mvc;

namespace PersonalFinances.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}