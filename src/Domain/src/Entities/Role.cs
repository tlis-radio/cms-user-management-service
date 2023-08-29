using System.Collections.Generic;
using Tlis.Cms.UserManagement.Domain.Entities.Base;

namespace Tlis.Cms.UserManagement.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<UserRoleHistory>? UserRoleHistory { get; set; }
}