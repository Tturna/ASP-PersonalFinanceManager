using Microsoft.AspNetCore.Mvc;
using PersonalFinances.Models;
using PersonalFinances.Models.DataTransferObjects;
using PersonalFinances.Services;

namespace PersonalFinances.Controllers;

public class RegisterController(AppDbContext dbContext, HashingService hashingService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register([FromForm] RegisterDto registerData)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", registerData);
        }

        var users = dbContext.Users.ToList();

        if (users.Any(u => u.Username == registerData.Username))
        {
            ModelState.AddModelError("Username", "Username already exists");
            return View("Index", registerData);
        }

        var passwordHash = hashingService.HashArgon2Id(registerData.Password);
        
        var newUser = new UserModel
        {
            Username = registerData.Username,
            Password = passwordHash,
            CreatedAt = DateTime.Now
        };

        dbContext.Users.Add(newUser);
        var stateChanges = dbContext.SaveChanges();
        
        if (stateChanges == 0)
        {
            ModelState.AddModelError("Username", "Failed to create user");
            return View("Index", registerData);
        }
        
        ViewBag.Message = "User created successfully";
        return View("Index");
    }
}