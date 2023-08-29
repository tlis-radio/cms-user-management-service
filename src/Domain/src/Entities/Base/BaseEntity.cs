using System;

namespace Tlis.Cms.UserManagement.Domain.Entities.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}