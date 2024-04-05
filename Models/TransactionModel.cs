using PersonalFinances.Models.DataTransferObjects;

namespace PersonalFinances.Models;

public class TransactionModel : TransactionDto
{
    public int Id { get; set; }
    
    // nav property
    public required UserModel UserModel { get; set; }
    // foreign key
    public required int UserModelId { get; set; }

    public TransactionModel Clone(DateOnly newDate)
    {
        return new TransactionModel()
        {
            UserModel = UserModel,
            UserModelId = UserModelId,
            IsIncome = IsIncome,
            AmountEuro = AmountEuro,
            Name = Name,
            Date = newDate,
            Category = Category,
            Reoccurrence = Reoccurrence
        };
    }
}