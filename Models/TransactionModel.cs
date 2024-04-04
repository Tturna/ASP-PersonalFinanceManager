using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public required decimal AmountEuro { get; set; }
    public required bool IsIncome { get; set; }
    [StringLength(20)]
    public string? Category { get; set; }
    [StringLength(20)]
    public required string Name { get; set; }
    // TODO: Consider a custom reoccurrance where the user can specify the interval (in days?)
    public Reoccurrance? Reoccurrance { get; set; }
    
    // nav property
    public required UserModel UserModel;
    // foreign key
    public required int UserModelId { get; set; }
}