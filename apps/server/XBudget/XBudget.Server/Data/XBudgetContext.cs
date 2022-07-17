using Microsoft.EntityFrameworkCore;
using XBudget.Server.App.Users;

namespace XBudget.Server.Data;

public class XBudgetContext : DbContext
{
    public XBudgetContext(DbContextOptions<XBudgetContext> contextOptions) : base(contextOptions)
    {
        
    }
    
    public DbSet<User> Users { get; set; } = null!;
}