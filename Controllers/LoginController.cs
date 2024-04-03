using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PersonalFinances.Models;
using PersonalFinances.Models.DataTransferObjects;
using PersonalFinances.Services;

namespace PersonalFinances.Controllers;

public class LoginController(AppDbContext dbContext, HashingService hashingService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginDto loginData)
    {
        var passwordHash = hashingService.HashArgon2Id(loginData.Password);

        var validUser = dbContext.Users.FirstOrDefault(u =>
            u.Username == loginData.Username &&
            u.PasswordHash == passwordHash
        );

        if (validUser == null)
        {
            ModelState.AddModelError("Username", "Username or password is incorrect");
            return View("Index", loginData);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, validUser.Username)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authProperties = new AuthenticationProperties()
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
            IsPersistent = true
        };

        await HttpContext.SignInAsync(claimsPrincipal, authProperties);
        
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index");
    }
}