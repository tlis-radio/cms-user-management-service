using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException(string? message = null)
        : base(message)
    {
    }
}