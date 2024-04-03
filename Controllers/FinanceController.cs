using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinances.Models.DataTransferObjects;

namespace PersonalFinances.Controllers;

[Authorize]
public class FinanceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddIncome()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddIncome(TransactionDto transactionData)
    {
        await Task.Delay(500);
        return RedirectToAction("AddIncome");
    }
    
    public IActionResult AddExpense()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddExpense(TransactionDto transactionData)
    {
        await Task.Delay(500);
        return RedirectToAction("AddExpense");
    }
}