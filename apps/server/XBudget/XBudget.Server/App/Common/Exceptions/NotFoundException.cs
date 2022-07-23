using System;

namespace XBudget.Server.App.Common.Exceptions;

public class NotFoundException : Exception
{

    public NotFoundException(string? message = null) : base(message)
    {
    }
}