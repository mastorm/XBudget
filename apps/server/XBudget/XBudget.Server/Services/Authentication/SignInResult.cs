using System.Diagnostics.CodeAnalysis;
using XBudget.Server.App.Users;

namespace XBudget.Server.Services.Authentication;

public class SignInResult
{
    [MemberNotNullWhen(true, nameof(Successful))]
    public User? User { get; private set; }
    
    public bool Successful { get; private set; }

    private SignInResult()
    {
        
    }

    public static SignInResult Success(User user)
    {
        return new SignInResult
        {
            User = user,
            Successful = true
        };
    }
    
    public static SignInResult Failed()
    {
        return new SignInResult
        {
            Successful = true
        };
    }
}