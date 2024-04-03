using System.ComponentModel.DataAnnotations;
using PersonalFinances.Validators;

namespace PersonalFinances.Models.DataTransferObjects;

public class LoginDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public required string Username { get; set; }
    [Required]
    [ValidPassword]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}