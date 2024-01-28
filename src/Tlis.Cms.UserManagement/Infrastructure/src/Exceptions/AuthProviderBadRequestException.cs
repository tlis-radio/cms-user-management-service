using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Exceptions;

public class AuthProviderBadRequestException(string message) : Exception(message)
{
}