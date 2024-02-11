using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

public interface IMembershipRepository : IGenericRepository<Membership>
{
    Task<Guid?> GetIdByStatus(MembershipStatus status);

    Task<List<Membership>> GetAll();
}