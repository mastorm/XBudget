using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using XBudget.Server.App.Users;

namespace XBudget.Server.Services.Authentication;

public class Session : ISession
{
    private readonly ILogger<Session> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly HttpContext? _context;

    public Session(
        ILogger<Session> logger,
        IPasswordHasher<User> passwordHasher, 
        IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _passwordHasher = passwordHasher;
        _context = contextAccessor.HttpContext;
    }

    public async Task<SignInResult> StartSessionAsync(User user, string password)
    {
        var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return verification switch
        {
            PasswordVerificationResult.Failed => SignInResult.Failed(),
            _ => await SignInAsync(user)
        };
    }

    private async Task<SignInResult> SignInAsync(User user)
    {
        if (_context is null)
        {   
            _logger.LogWarning("Sign in of user {Email} failed due to missing HttpContext", user.Email);
            return SignInResult.Failed();
        }
        
        var principal = new ClaimsPrincipal(user.Identity(CookieAuthenticationDefaults.AuthenticationScheme));
        await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
        {
            IsPersistent = true
        });
        
        return SignInResult.Success(user);
    }
}