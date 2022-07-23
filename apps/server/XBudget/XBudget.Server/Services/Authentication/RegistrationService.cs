using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XBudget.Server.App.Users;
using XBudget.Server.Data;
using XBudget.Server.Services.Authentication.Exceptions;

namespace XBudget.Server.Services.Authentication;

public class RegistrationService : IRegistrationService, IAsyncDisposable
{
    private readonly XBudgetContext _context;
    private readonly IPasswordHasher<User?> _hasher;

    public RegistrationService(IDbContextFactory<XBudgetContext> context, IPasswordHasher<User?> hasher)
    {
        _context = context.CreateDbContext();
        _hasher = hasher;
    }


    public async Task<User?> RegisterAsync(RegistrationPayload payload)
    {
        await CheckEmailAvailabilityAsync(payload.Email);

        var user = new User(payload.Email, "");
        var hash = _hasher.HashPassword(user, payload.Password);
        user.PasswordHash = hash;
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    private async Task CheckEmailAvailabilityAsync(string email)
    {
        if (await _context.Users.AnyAsync(user => user.Email == email))
        {
            throw new EmailNotAvailableException();
        }
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}