using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalFinances.Models.Enums;

namespace PersonalFinances.Models.DataTransferObjects;

public class TransactionDto
{
    [Required]
    public bool IsIncome { get; set; }
    
    [Required, DataType(DataType.Currency), Range(0, double.MaxValue)]
    public decimal AmountEuro { get; set; }

    [Required, MinLength(3), MaxLength(20)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string? Category { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateOnly Date { get; set; }
    [DataType(DataType.Date)]
    public DateOnly? CancelDate { get; set; }
    public Reoccurrence? Reoccurrence { get; set; }

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