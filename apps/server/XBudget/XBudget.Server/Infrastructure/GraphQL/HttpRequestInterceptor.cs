using System;
using System.Security.Claims;
using System.Threading;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;

namespace XBudget.Server.Infrastructure.GraphQL;

public class HttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        var userId =
            context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(userId, out var guid))
        {
            requestBuilder.SetProperty("UserId", guid);
        }
        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}