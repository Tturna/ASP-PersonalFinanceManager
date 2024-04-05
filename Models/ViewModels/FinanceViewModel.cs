namespace PersonalFinances.Models.ViewModels;

public class FinanceViewModel
{
    public class TransactionGroup
    {
        public required string MonthYear { get; set; }
        public required List<TransactionModel> Transactions { get; set; }
        public required decimal TotalIncome { get; set; }
        public required decimal TotalExpenses { get; set; }
        public required decimal Total { get; set; }
    }
    
    public required List<TransactionModel> IncomeTransactions { get; set; }
    public required List<TransactionModel> ExpenseTransactions { get; set; }
    public required List<TransactionModel> AllTransactions { get; set; }
    public required IEnumerable<TransactionGroup> SortedTransactionGroups { get; set; }
}