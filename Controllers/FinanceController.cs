using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;
using PersonalFinances.Models.DataTransferObjects;
using PersonalFinances.Services;

namespace PersonalFinances.Controllers;

[Authorize]
public class FinanceController(AppDbContext dbContext) : Controller
{
    public IActionResult Index()
    {
        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // user can't be null because of the [Authorize] attribute
        var userWithTransactions = dbContext.Users
            .Include(u => u.Transactions)
            .FirstOrDefault(u => u.Username == userName);

        return View(userWithTransactions!.Transactions);
    }

    public IActionResult AddTransaction()
    {
        return View(new TransactionDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTransaction(TransactionDto transactionData)
    {
        if (!ModelState.IsValid)
        {
            return View(transactionData);
        }

        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // user can't be null because of the [Authorize] attribute
        var userWithTransactions = dbContext.Users
            .Include(u => u.Transactions)
            .FirstOrDefault(u => u.Username == userName);

        var existingTransaction = userWithTransactions!.Transactions
            .FirstOrDefault(t => t.Name == transactionData.Name);

        if (existingTransaction != null)
        {
            ModelState.AddModelError("Name", "Transaction with this name already exists");
            return View(transactionData);
        }

        var newTransaction = new TransactionModel
        {
            User = userWithTransactions,
            UserId = userWithTransactions.Id,
            IsIncome = transactionData.IsIncome,
            Name = transactionData.Name,
            AmountEuro = transactionData.AmountEuro,
            Category = transactionData.Category,
            Reoccurrance = transactionData.Reoccurrence,
        };

        dbContext.Transactions.Add(newTransaction);
        var stateChanges = await dbContext.SaveChangesAsync();

        if (stateChanges == 0)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong saving the transaction. Please try again.");
            return View(transactionData);
        }

        return RedirectToAction("AddTransaction");
    }
}