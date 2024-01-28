using System;

namespace Tlis.Cms.UserManagement.Application.Exceptions;

internal sealed class UserRoleNotFoundException(Guid roleId)
    : Exception($"Role with id: {roleId} not found in Cache or Db")
{
}