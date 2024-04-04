using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalFinances.Models.DataTransferObjects;

public class TransactionDto
{
    // hidden field
    [Required]
    public bool IsIncome { get; set; }
    
    [Required, DataType(DataType.Currency), Range(0, float.MaxValue)]
    public float AmountEuro { get; set; }

    [Required, MinLength(3), MaxLength(20)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string? Category { get; set; }
    public Reoccurrance? Reoccurrence { get; set; }

    public List<SelectListItem> ReoccurrenceOptions { get; set; }
    public List<SelectListItem> IsIncomeOptions { get; set; }

    public TransactionDto()
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