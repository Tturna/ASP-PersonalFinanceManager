namespace PersonalFinances.Models.ViewModels;

public class FinanceViewModel
{
    public List<TransactionModel> IncomeTransactions { get; set; } = [];
    public List<TransactionModel> ExpenseTransactions { get; set; } = [];
    public List<TransactionModel> AllTransactions => IncomeTransactions.Concat(ExpenseTransactions).ToList();
}