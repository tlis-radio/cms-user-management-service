using System;
using Tlis.Cms.UserManagement.Domain.Constants;

namespace Tlis.Cms.UserManagement.Application.Exceptions;

public sealed class MembershipNotFoundException : Exception
{
    public MembershipNotFoundException(MembershipStatus membership)
        : base($"Membership: {membership} not found in Cache or Db"){}

    public MembershipNotFoundException(Guid membershipId)
        : base($"Membership: {membershipId} not found in Cache or Db"){}  
}