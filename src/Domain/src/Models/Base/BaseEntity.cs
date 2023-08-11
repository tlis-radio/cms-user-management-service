using System;

namespace Tlis.Cms.UserManagement.Domain.Models.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}