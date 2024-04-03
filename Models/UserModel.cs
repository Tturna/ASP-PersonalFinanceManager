using System.ComponentModel.DataAnnotations;

namespace PersonalFinances.Models;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    [StringLength(20)]
    public required string Username { get; set; }
    public required byte[] Password { get; set; }
    public DateTime CreatedAt { get; set; }
}