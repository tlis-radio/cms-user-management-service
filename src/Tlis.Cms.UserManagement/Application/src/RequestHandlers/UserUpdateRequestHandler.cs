using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Mappers;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserUpdateRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UserUpdateRequest, bool>
{
    public async Task<bool> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserDetailsById(request.Id, asTracking: true);
        if (user is null) return false;

        user.Firstname = request.Firstname;
        user.Lastname = request.Lastname;
        user.Nickname = request.Nickname;
        user.Abouth = request.Abouth;
        user.PreferNicknameOverName = request.PreferNicknameOverName;
        ResolveRoleHistory(user, request.RoleHistory);
        ResolveMembershipHistory(user, request.MembershipHistory);

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    private static void ResolveRoleHistory(User existing, List<UserUpdateRequestRoleHistory> updatedHistory)
    {
        var added = updatedHistory.Where(x => x.Id is null).Select(UserMapper.ToEntity);

        var existingOptionsDict = existing.RoleHistory.ToDictionary(key => key.Id, value => value);
        var existingOptionsUpdated = updatedHistory.Where(x => x.Id is not null).Select(x => UserMapper.ToExistingEntity(existingOptionsDict[x.Id!.Value], x));

        existing.RoleHistory = added.Concat(existingOptionsUpdated).ToList();
    }

    private static void ResolveMembershipHistory(User existing, List<UserUpdateRequestMembershipHistory> updatedHistory)
    {
        var added = updatedHistory.Where(x => x.Id is null).Select(UserMapper.ToEntity);

        var existingOptionsDict = existing.MembershipHistory.ToDictionary(key => key.Id, value => value);
        var existingOptionsUpdated = updatedHistory.Where(x => x.Id is not null).Select(x => UserMapper.ToExistingEntity(existingOptionsDict[x.Id!.Value], x));

        existing.MembershipHistory = added.Concat(existingOptionsUpdated).ToList();
    }
}