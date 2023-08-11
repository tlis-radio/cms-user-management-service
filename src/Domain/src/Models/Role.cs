using System.Collections.Generic;
using Tlis.Cms.UserManagement.Domain.Models.Base;

namespace Tlis.Cms.UserManagement.Domain.Models;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<UserRoleHistory>? UserRoleHistory { get; set; }
}