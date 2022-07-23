using Microsoft.AspNetCore.Builder;
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

    public static async Task UseAutoMigration<T>(this IApplicationBuilder app) where T: DbContext
    {
        var factory = app.ApplicationServices.GetRequiredService<IDbContextFactory<T>>();
        await using var ctx = await factory.CreateDbContextAsync(); 
        await ctx.Database.MigrateAsync();
    }
}