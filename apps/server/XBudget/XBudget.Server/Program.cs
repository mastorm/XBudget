using HotChocolate.Data;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XBudget.Server.App.Users;
using XBudget.Server.Data;
using XBudget.Server.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
config.AddJsonFile("appsettings.Development.local.json", optional: true);

var services = builder.Services;


services
    .AddGraphQLServer()
    .AddServerTypes()
    .AddQueryType<Query>()
    .AddMutationType()
    .RegisterDbContext<XBudgetContext>(DbContextKind.Pooled)
    
    .AddMutationConventions();

services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
services.AddScoped<ISession, Session>();
services.AddScoped<IRegistrationService, RegistrationService>();

var app = builder.Build();

app.UseRouting();

app.MapGraphQL();

app.Run();

class Query
{
    public string Placeholder() => "remove later";
}