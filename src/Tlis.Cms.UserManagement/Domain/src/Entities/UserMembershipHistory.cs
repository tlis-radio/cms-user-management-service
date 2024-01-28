using System;
using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Entities.Base;

namespace Tlis.Cms.UserManagement.Domain.Entities;

public class UserMembershipHistory : BaseEntity
{
    public Guid UserId { get; set; }

    public MembershipStatus Status { get; set; }

    public DateTime ChangeDate { get; set; }

    public string? Description { get; set; }
}