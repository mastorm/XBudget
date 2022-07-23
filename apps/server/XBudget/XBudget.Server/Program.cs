using HotChocolate.Data;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XBudget.Server;
using XBudget.Server.App.Users;
using XBudget.Server.Configuration;
using XBudget.Server.Data;
using XBudget.Server.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
config.AddJsonFile("appsettings.Development.local.json", optional: true);

var services = builder.Services;


services
    .AddGraphQLServer()
    .AddMutationType()
    .AddQueryType<Query>()
    .AddServerTypes()
    .RegisterDbContext<XBudgetContext>(DbContextKind.Pooled);
    // .AddMutationConventions();

services.AddDatabase(config.GetConnectionString("SQL"));
services.AddHttpContextAccessor();
services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
services.AddTransient<ISession, Session>();
services.AddTransient<IRegistrationService, RegistrationService>();

var app = builder.Build();

await app.UseAutoMigration<XBudgetContext>();

app.UseRouting();

app.MapGraphQL();

app.Run();

