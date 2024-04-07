using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalFinances.Models.DataTransferObjects;

public enum Reoccurrence
{
    None,
    Daily,
    Weekly,
    Monthly,
    Annually
}

public class TransactionDto
{
    [Required]
    public bool IsIncome { get; init; }
    
    [Required, DataType(DataType.Currency), Range(0, double.MaxValue)]
    public decimal AmountEuro { get; init; }

    [Required, MinLength(3), MaxLength(20)]
    public string Name { get; init; } = string.Empty;
    
    [StringLength(20)]
    public string? Category { get; init; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateOnly Date { get; init; }
    public DateOnly? CancelDate { get; init; }
    public Reoccurrence? Reoccurrence { get; init; }

    [BindNever]
    public static List<SelectListItem> ReoccurrenceOptions { get; }
    [BindNever]
    public static List<SelectListItem> IsIncomeOptions { get; }

    static TransactionDto()
    {
        ReoccurrenceOptions = Enum.GetValues<Reoccurrence>()
            .Select(r => new SelectListItem
            {
                Value = r.ToString(),
                Text = r.ToString()
            })
            .ToList();

        IsIncomeOptions = new List<SelectListItem>()
        {
            new("Income", "true"),
            new("Expense", "false")
        };
    }
}