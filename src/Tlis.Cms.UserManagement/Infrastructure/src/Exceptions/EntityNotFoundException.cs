using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class EntityNotFoundException(string? message = null) : Exception(message)
{
}