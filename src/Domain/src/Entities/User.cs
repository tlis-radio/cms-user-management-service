using System;
using System.Collections.Generic;
using Tlis.Cms.UserManagement.Domain.Entities.Base;

namespace Tlis.Cms.UserManagement.Domain.Entities;

public class User : BaseEntity
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ProfileImageUrl { get; set; }

    public bool IsActive { get; set; }

    public DateOnly MemberSinceDate { get; set; }

    public DateOnly? MembershipEndedDate { get; set; }

    public string? MembershipEndedReason { get; set; }

    public string? ExternalId { get; set; }

    public string? Email { get; set; }
    
    public virtual ICollection<UserRoleHistory>? RoleHistory { get; set; }
}