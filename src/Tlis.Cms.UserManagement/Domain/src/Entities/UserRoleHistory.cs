using System;
using Tlis.Cms.UserManagement.Domain.Entities.Base;

namespace Tlis.Cms.UserManagement.Domain.Entities;

public class UserRoleHistory : BaseEntity
{
    public DateTime FunctionStartDate { get; set; }

    public DateTime? FunctionEndDate { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public virtual User? User { get; set; }

    public virtual Role? Role { get; set; }
}