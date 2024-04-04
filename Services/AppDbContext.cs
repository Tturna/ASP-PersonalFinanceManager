using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;

namespace PersonalFinances.Services;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // "default!" tells the compiler that the property will not be null. EF initializes it.
    public DbSet<UserModel> Users { get; set; } = default!;
    public DbSet<TransactionModel> Transactions { get; set; } = default!;
}