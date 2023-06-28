using CoupleCoinApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoupleCoinApi.dbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Couple> Couple { get; set; }
        public DbSet<ExpenseType> ExpenseType { get; set; }
    }
}
