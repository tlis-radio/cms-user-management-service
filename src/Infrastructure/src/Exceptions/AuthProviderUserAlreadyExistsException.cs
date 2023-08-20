using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class AuthProviderUserAlreadyExistsException : Exception
{
    public AuthProviderUserAlreadyExistsException(string message) : base(message)
    {
    }
}