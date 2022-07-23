using System;
using System.Linq;
using System.Security.Claims;
using XBudget.Server.Data;

namespace XBudget.Server.App.Users;

public class User : Entity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    [GraphQLIgnore]
    public string PasswordHash { get; set; }
    public bool EmailConfirmed { get; set; } = false;
    
    public User(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }
    
    protected User()
    {
        Email = null!;
        PasswordHash = null!;
    }

    [GraphQLIgnore]
    public ClaimsIdentity Identity(string scheme) => new(
        new Claim[]
        {
            new(ClaimTypes.NameIdentifier, Id.ToString())

        },
        scheme);

}