using System;
using Tlis.Cms.UserManagement.Domain.Constants;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserDetailsGetResponseUserMembershipHistoryMembership
{
    public Guid Id { get; set; }

    public MembershipStatus Status { get; set; }
}