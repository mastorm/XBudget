
using System;
using Microsoft.EntityFrameworkCore;
using XBudget.Server.Data;
using XBudget.Server.Infrastructure.GraphQL;

namespace XBudget.Server.App.Users.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class UserQueries
{
    [Authorize]
    public async Task<User?> MeAsync(XBudgetContext context, [UserId] Guid userId)
        => await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

}