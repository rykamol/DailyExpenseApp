using ExpenseManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Data
{
    public class ExpenseDbContext:DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { 
        }

        public DbSet<ExpenseModel> Expenses { get; set; }
    }
}