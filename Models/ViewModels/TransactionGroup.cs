namespace PersonalFinances.Models.ViewModels;

public class TransactionGroup
{
    public required string MonthYear { get; init; }
    public bool IsCurrentMonth { get; init; }
    public List<TransactionModel> Transactions { get; set; } = [];
    public decimal TotalIncome { get; init; }
    public decimal TotalExpenses { get; init; }
    public decimal Total { get; init; }
}