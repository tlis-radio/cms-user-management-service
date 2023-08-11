using System;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Models;

namespace Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

public interface IRoleService
{
    Task<Role?> GetByIdAsync(Guid id);
}