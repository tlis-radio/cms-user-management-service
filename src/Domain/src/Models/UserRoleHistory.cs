using System;
using Tlis.Cms.UserManagement.Domain.Models.Base;

namespace Tlis.Cms.UserManagement.Domain.Models;

public class UserRoleHistory : BaseEntity
{
    public DateOnly FunctionStartDate { get; set; }

    public DateOnly? FunctionEndDate { get; set; }

    public Guid UserForeignKey { get; set; }

    public Guid RoleForeignKey { get; set; }

    public virtual User? User { get; set; }

    public virtual Role? Role { get; set; }
}