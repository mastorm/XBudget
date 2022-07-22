using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XBudget.Server.Data;

namespace XBudget.Server.Configuration;

public static class ConfigureDatabase
{
    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddPooledDbContextFactory<XBudgetContext>(builder => builder.UseNpgsql(connectionString));
    }
}