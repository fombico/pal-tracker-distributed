using Microsoft.EntityFrameworkCore;

namespace Expenses
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options)
        {
        }

        public DbSet<ExpenseRecord> ExpenseRecords { get; set; }
    }
}
