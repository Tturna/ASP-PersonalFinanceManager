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
    public required float AmountEuro { get; set; }
    public required bool IsIncome { get; set; }
    [StringLength(20)]
    public string? Category { get; set; }
    [StringLength(20)]
    public required string Name { get; set; }
    // TODO: Consider a custom reoccurrance where the user can specify the interval (in days?)
    public Reoccurrance? Reoccurrance { get; set; }
    
    // foreign key
    public int UserId { get; set; }
    // nav property
    public required UserModel User;
}