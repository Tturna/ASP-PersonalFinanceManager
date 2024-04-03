using Microsoft.EntityFrameworkCore;

namespace PersonalFinances.Models;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // "default!" tells the compiler that the property will not be null. EF initializes it.
    public DbSet<UserModel> Users { get; set; } = default!;
}