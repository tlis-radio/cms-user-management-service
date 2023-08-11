using System;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Models;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICacheService _cacheService;

    public RoleService(
        IUnitOfWork unitOfWork,
        ICacheService cacheService
    )
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        var cacheKey = CreateRoleCacheKey(id);
        if (_cacheService.TryGetValue<Role>(cacheKey, out var role) is false)
        {
            role = await _unitOfWork.RoleRepository.GetByIdAsync(id, asTracking: false);
            if (role is null)
                return null;

            _cacheService.Set(cacheKey, role);
            return role;
        }

        return role;
    }

    private string CreateRoleCacheKey(Guid id) => $"role-{id}";
}