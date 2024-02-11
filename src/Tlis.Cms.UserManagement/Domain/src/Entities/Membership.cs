using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Entities.Base;

namespace Tlis.Cms.UserManagement.Domain.Entities;

public class Membership : BaseEntity
{
    public MembershipStatus Status { get; set; }
}