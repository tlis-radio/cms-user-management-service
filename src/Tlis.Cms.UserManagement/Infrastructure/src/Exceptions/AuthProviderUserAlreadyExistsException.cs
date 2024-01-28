using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class AuthProviderUserAlreadyExistsException(string message) : Exception(message)
{
}