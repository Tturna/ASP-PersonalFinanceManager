using System.ComponentModel.DataAnnotations;

namespace PersonalFinances.Models;

public enum Reoccurrance
{
    Daily,
    Weekly,
    Monthly,
    Annually
}

public class TransactionModel
{
    [Key]
    public int Id { get; set; }
    public required int AmountEuroCents { get; set; }
    public required bool IsIncome { get; set; }
    [StringLength(20)]
    public string? Category { get; set; }
    // TODO: Consider a custom reoccurrance where the user can specify the interval (in days?)
    public Reoccurrance? Reoccurrance { get; set; }
    
    public int UserId { get; set; }
    public required UserModel User;
}