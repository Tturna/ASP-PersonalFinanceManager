using System.ComponentModel.DataAnnotations;

namespace PersonalFinances.Models.DataTransferObjects;

public class SavingGoalDto
{
    [DataType(DataType.Currency)]
    [Range(0, 1000_000_000)]
    public decimal MonthlySavingGoal { get; set; }
}