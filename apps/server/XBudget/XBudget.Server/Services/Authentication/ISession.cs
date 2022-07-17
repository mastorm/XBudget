using XBudget.Server.App.Users;

namespace XBudget.Server.Services.Authentication;

public interface ISession
{
    Task<SignInResult> StartSessionAsync(User user, string password);
}