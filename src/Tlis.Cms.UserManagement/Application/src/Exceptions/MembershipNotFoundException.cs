using System;
using Tlis.Cms.UserManagement.Domain.Constants;

namespace Tlis.Cms.UserManagement.Application.Exceptions;

public sealed class MembershipNotFoundException(MembershipStatus membership)
    : Exception($"Membership: {membership} not found in Cache or Db")
{
}