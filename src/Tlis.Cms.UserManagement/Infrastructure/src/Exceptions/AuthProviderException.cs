using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class AuthProviderException(string message) : Exception(message)
{
}