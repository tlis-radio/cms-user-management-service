using System;

namespace Tlis.Cms.UserManagement.Application.Exceptions;

internal sealed class UserRoleNotFoundException : Exception
{
    public UserRoleNotFoundException(Guid roleId)
        : base($"Role with id: {roleId} not found in Cache or Db")
    { }
}