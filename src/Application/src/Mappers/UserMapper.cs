using Riok.Mapperly.Abstractions;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Domain.Models;

namespace Tlis.Cms.UserManagement.Application.Mappers;

[Mapper]
internal partial class UserMapper
{
    public User ToEntity(UserCreateRequest dto)
    {
        var entity = MapToUser(dto);

        entity.IsActive = true;
        
        return entity;
    }
    
    [MapperIgnoreTarget(nameof(User.IsActive))]
    [MapperIgnoreTarget(nameof(User.ProfileImageUrl))]
    [MapperIgnoreTarget(nameof(User.ExternalId))]
    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreSource(nameof(ArchiveUserCreateRequest.Password))]
    public partial User ToEntity(ArchiveUserCreateRequest dto);
    
    [MapperIgnoreTarget(nameof(User.IsActive))]
    [MapperIgnoreTarget(nameof(User.ProfileImageUrl))]
    [MapperIgnoreTarget(nameof(User.ExternalId))]
    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.MembershipEndedDate))]
    [MapperIgnoreTarget(nameof(User.MembershipEndedReason))]
    [MapperIgnoreTarget(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(ArchiveUserCreateRequest.Password))]
    [MapperIgnoreSource(nameof(UserCreateRequest.RoleId))]
    [MapperIgnoreSource(nameof(UserCreateRequest.FunctionStartDate))]
    private partial User MapToUser(UserCreateRequest dto);

    [MapProperty(nameof(ArchiveUserRoleHistoryCreateRequest.RoleId), nameof(UserRoleHistory.RoleForeignKey))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.UserForeignKey))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.User))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Role))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Id))]
    private partial UserRoleHistory MapToUserRoleHistory(ArchiveUserRoleHistoryCreateRequest dto);
}