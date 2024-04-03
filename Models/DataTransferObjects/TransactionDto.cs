using System.ComponentModel.DataAnnotations;

namespace PersonalFinances.Models.DataTransferObjects;

public class TransactionDto
{
    [Required]
    [DataType(DataType.Currency)]
    [Range(0, int.MaxValue)]
    public int AmountEuroCents { get; set; }
    
    // This should be automatically set as a hidden form field in the AddIncome and AddExpense views
    [Required]
    public bool IsIncome { get; set; }
    
    [StringLength(20)]
    public string? Category { get; set; }
    public Reoccurrance? Reoccurrance { get; set; }
}