using PersonalFinances.Models.DataTransferObjects;

namespace PersonalFinances.Models.ViewModels;

public class AnalyticsViewModel
{
    public List<TransactionGroup> SortedTransactionGroups { get; set; } = [];
    public SavingGoalDto SavingGoal { get; set; } = new();
    
    public AnalyticsViewModel() { }

    public AnalyticsViewModel(UserModel userWithTransactions)
    {
        var transactions = userWithTransactions.TransactionModels;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var currentMonthYear = today.ToString("MMMM yyyy");

        SavingGoal.MonthlySavingGoal = userWithTransactions.MonthlySavingGoal ?? 0;
        
        // group transactions by year and month
        // TODO: Refactor to use a shared method with FinanceViewModel
        SortedTransactionGroups = transactions
            .Where(t => t.Date <= today)
            .OrderByDescending(t => t.Date)
            .GroupBy(t => t.Date.ToString("MMMM yyyy"))
            .Select(g => new TransactionGroup
            {
                MonthYear = g.Key,
                IsCurrentMonth = g.Key == currentMonthYear,
                Transactions = g.ToList(),
                TotalIncome = g.Where(t => t.IsIncome).Sum(t => t.AmountEuro),
                TotalExpenses = g.Where(t => !t.IsIncome).Sum(t => t.AmountEuro),
                Total = g.Sum(t => t.AmountEuro * (t.IsIncome ? 1 : -1))
            })
            .ToList();
    }
}