using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class AuthProviderException : Exception
{
    public AuthProviderException(string message) : base(message)
    {
    }
}