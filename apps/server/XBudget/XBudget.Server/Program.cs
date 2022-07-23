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

services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
services.AddScoped<ISession, Session>();
services.AddScoped<IRegistrationService, RegistrationService>();

var app = builder.Build();

app.UseRouting();

app.MapGraphQL();

app.Run();

