using System.ComponentModel.DataAnnotations;
using PersonalFinances.Validators;

namespace PersonalFinances.Models.DataTransferObjects;

public class RegisterDto : LoginDto
{
    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }
}