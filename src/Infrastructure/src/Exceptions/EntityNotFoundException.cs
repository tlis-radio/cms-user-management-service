using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string? message = null) : base(message)
    {
    }
}