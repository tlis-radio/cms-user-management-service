using Riok.Mapperly.Abstractions;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Application.Mappers;

[Mapper]
internal static partial class UserMapper
{
    [MapperIgnoreSource(nameof(User.ExternalId))]
    [MapperIgnoreSource(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(User.Abouth))]
    [MapperIgnoreSource(nameof(User.ProfileImageId))]
    [MapperIgnoreSource(nameof(User.PreferNicknameOverName))]
    public static partial UserFilterGetResponse ToFilterDto(User entity);

    [MapperIgnoreSource(nameof(User.Id))]
    public static partial UserDetailsGetResponse? ToDto(User? entity);
    
    [MapperIgnoreSource(nameof(User.ExternalId))]
    [MapperIgnoreSource(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(User.Abouth))]
    [MapperIgnoreSource(nameof(User.ProfileImageId))]
    [MapperIgnoreSource(nameof(User.PreferNicknameOverName))]
    public static partial UserPaginationGetResponse ToPaginationDto(User entity);

    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.ProfileImageId))]
    [MapperIgnoreTarget(nameof(User.ExternalId))]
    [MapperIgnoreTarget(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(UserCreateRequest.Password))]
    [MapperIgnoreSource(nameof(UserCreateRequest.MembershipHistory))]
    public static partial User ToEntity(UserCreateRequest dto);
    
    [MapProperty(nameof(UserRoleHistoryCreateRequest.RoleId), nameof(UserRoleHistory.RoleId))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.UserId))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.User))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Role))]
    [MapperIgnoreTarget(nameof(UserRoleHistory.Id))]
    private static partial UserRoleHistory MapToUserRoleHistory(UserRoleHistoryCreateRequest dto);

    [MapperIgnoreSource(nameof(UserRoleHistory.UserId))]
    [MapperIgnoreSource(nameof(UserRoleHistory.RoleId))]
    [MapperIgnoreSource(nameof(UserRoleHistory.User))]
    [MapperIgnoreSource(nameof(UserRoleHistory.Id))]
    private static partial UserDetailsGetResponseUserRoleHistory MapToUserDetailsGetResponseUserRoleHistory(UserRoleHistory entity);

    [MapperIgnoreSource(nameof(Role.Id))]
    private static partial UserDetailsGetResponseRole MapToUserDetailsGetResponseRole(Role role);
    
    [MapperIgnoreSource(nameof(UserMembershipHistory.UserId))]
    [MapperIgnoreSource(nameof(UserMembershipHistory.MembershipId))]
    [MapperIgnoreSource(nameof(UserMembershipHistory.Id))]
    private static partial UserDetailsGetResponseUserMembershipHistory MapToUserDetailsGetResponseUserMembershipHistory(UserMembershipHistory entity);
}