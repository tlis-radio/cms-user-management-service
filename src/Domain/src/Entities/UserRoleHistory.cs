using System;
using Tlis.Cms.UserManagement.Domain.Entities.Base;

namespace Tlis.Cms.UserManagement.Domain.Entities;

public class UserRoleHistory : BaseEntity
{
    public DateOnly FunctionStartDate { get; set; }

    public DateOnly? FunctionEndDate { get; set; }

    public Guid UserForeignKey { get; set; }

    public Guid RoleForeignKey { get; set; }

    public virtual User? User { get; set; }

    public virtual Role? Role { get; set; }
}