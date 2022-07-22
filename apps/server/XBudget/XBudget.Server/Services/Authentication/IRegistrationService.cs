using XBudget.Server.App.Users;

namespace XBudget.Server.Services.Authentication;

public interface IRegistrationService
{
    Task<User> RegisterAsync(RegistrationPayload payload);
}