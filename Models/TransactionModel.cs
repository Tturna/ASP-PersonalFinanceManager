using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinances.Models.DataTransferObjects;

namespace PersonalFinances.Models;

public class TransactionModel : TransactionDto
{
    public int Id { get; set; }
    
    // nav property
    public required UserModel UserModel { get; set; }
    // foreign key
    public required int UserModelId { get; set; }
}