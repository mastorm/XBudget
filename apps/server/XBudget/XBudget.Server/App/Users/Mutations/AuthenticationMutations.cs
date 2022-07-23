using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using XBudget.Server.App.Common.Exceptions;
using XBudget.Server.App.Users.Exceptions;
using XBudget.Server.Data;
using XBudget.Server.Services.Authentication;
using XBudget.Server.Services.Authentication.Exceptions;

namespace XBudget.Server.App.Users.Mutations;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class AuthenticationMutations
{
    private readonly ISession _session;

    public AuthenticationMutations(
        ISession session
        )
    {
        _session = session;
    }

    [Error<NotFoundException>]
    [Error<LoginFailedException>]
    public async Task<User?> SignInAsync(
        XBudgetContext context,
        [EmailAddress] string email,
        string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == email);
    
        if (user is null)
        {
            throw new NotFoundException(nameof(User));
        }
    
        var signInResult = await _session.StartSessionAsync(user, password);
        return signInResult switch
        {
            { Successful: true } => signInResult.User,
            _ => throw new LoginFailedException()
        };
    }

    [Error<EmailNotAvailableException>]
    public Task<User?> RegisterAsync(
        [Service] IRegistrationService registrationService,
        [EmailAddress] string email,
        [MinLength(8)] string password
        )
    {
        return registrationService.RegisterAsync(new RegistrationPayload(email, password));
    }
}