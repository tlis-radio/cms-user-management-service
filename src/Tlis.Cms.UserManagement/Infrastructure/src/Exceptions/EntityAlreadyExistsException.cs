using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class EntityAlreadyExistsException(string? message = null) : Exception(message)
{
}