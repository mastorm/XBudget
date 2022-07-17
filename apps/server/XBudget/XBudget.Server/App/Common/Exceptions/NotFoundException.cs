using System;

namespace XBudget.Server.App.Common.Exceptions;

public class NotFoundException<T> : Exception
{
    public string? Entity { get; private set; } = typeof(T).FullName;

    public NotFoundException(string? message = null, string? typeName = null) : base(message)
    {
        if (typeName != null)
        {
            Entity = typeName;
        }
    }
}