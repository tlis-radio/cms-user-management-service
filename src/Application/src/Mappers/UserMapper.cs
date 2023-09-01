using Riok.Mapperly.Abstractions;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Entities;

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

    [MapperIgnoreSource(nameof(User.Id))]
    public partial UserDetailsGetResponse? ToDto(User? entity);
    
    [MapperIgnoreSource(nameof(User.Id))]
    public partial UserPaginationGetResponse ToPaginationDto(User entity);
    
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

    [MapperIgnoreSource(nameof(UserRoleHistory.UserForeignKey))]
    [MapperIgnoreSource(nameof(UserRoleHistory.RoleForeignKey))]
    [MapperIgnoreSource(nameof(UserRoleHistory.User))]
    [MapperIgnoreSource(nameof(UserRoleHistory.Id))]
    private partial UserDetailsGetResponseUserRoleHistory MapToUserDetailsGetResponseUserRoleHistory(UserRoleHistory entity);

    [MapperIgnoreSource(nameof(Role.Id))]
    [MapperIgnoreSource(nameof(Role.UserRoleHistory))]
    private partial UserDetailsGetResponseRole MapToUserDetailsGetResponseRole(Role role);
    
    [MapperIgnoreSource(nameof(UserRoleHistory.UserForeignKey))]
    [MapperIgnoreSource(nameof(UserRoleHistory.RoleForeignKey))]
    [MapperIgnoreSource(nameof(UserRoleHistory.User))]
    [MapperIgnoreSource(nameof(UserRoleHistory.Id))]
    private partial UserPaginationGetResponseUserRoleHistory MapToUserPaginationGetResponseUserRoleHistory(UserRoleHistory entity);

    [MapperIgnoreSource(nameof(Role.Id))]
    [MapperIgnoreSource(nameof(Role.UserRoleHistory))]
    private partial UserPaginationGetResponseRole MapToUserPaginationGetResponseRole(Role role);
}