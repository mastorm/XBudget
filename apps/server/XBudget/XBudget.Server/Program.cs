using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XBudget.Server.App.Users;
using XBudget.Server.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;


services
    .AddGraphQLServer()
    .AddQueryType()
    .AddQueryType()
    .AddMutationType()
    .AddMutationConventions();

services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
services.AddScoped<ISession, Session>();

var app = builder.Build();

app.MapGraphQL();

app.Run();

