﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PersonalFinances.Models.DataTransferObjects;

namespace PersonalFinances.Models;

public class TransactionModel : TransactionDto
{
    public int Id { get; set; }
    public bool IsAutoGenerated { get; set; }
    
    // nav property
    [ValidateNever]
    public required UserModel UserModel { get; set; }
    // foreign key
    public required int UserModelId { get; set; }

    public TransactionModel Clone(DateOnly newDate)
    {
        return new TransactionModel()
        {
            // Id = Id,
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