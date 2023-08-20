using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class AuthProviderBadRequestException : Exception
{
    public AuthProviderBadRequestException(string message) : base(message)
    {
    }
}