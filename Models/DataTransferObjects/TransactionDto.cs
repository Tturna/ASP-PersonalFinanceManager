using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalFinances.Models.DataTransferObjects;

public enum Reoccurrance
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
    public required bool IsIncome { get; set; }
    
    [Required, DataType(DataType.Currency), Range(0, double.MaxValue)]
    public required decimal AmountEuro { get; set; }

    [Required, MinLength(3), MaxLength(20)]
    public required string Name { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string? Category { get; set; }
    public Reoccurrance? Reoccurrence { get; set; }

    public static List<SelectListItem> ReoccurrenceOptions { get; set; }
    public static List<SelectListItem> IsIncomeOptions { get; set; }

    static TransactionDto()
    {
        ReoccurrenceOptions = Enum.GetValues<Reoccurrance>()
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