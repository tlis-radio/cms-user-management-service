using System;
using System.Linq;
using Riok.Mapperly.Abstractions;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Application.Mappers;

[Mapper]
internal static partial class UserMapper
{
    public static UserPaginationGetResponse ToPaginationDto(User entity)
    {
        var resposne = MapToPaginationDto(entity);

        resposne.Roles = entity.RoleHistory.Select(x => x.Role!.Name).ToList();

        var latestMembership = entity.MembershipHistory.OrderByDescending(x => x.ChangeDate).FirstOrDefault();

        if (latestMembership != null && latestMembership.Membership != null)
        {
            resposne.Status = Enum.GetName(latestMembership.Membership.Status);
        }

        return resposne;
    }

    [MapperIgnoreSource(nameof(User.ExternalId))]
    [MapperIgnoreSource(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(User.Abouth))]
    [MapperIgnoreSource(nameof(User.ProfileImageId))]
    [MapperIgnoreSource(nameof(User.PreferNicknameOverName))]
    public static partial UserFilterGetResponse ToFilterDto(User entity);

    [MapperIgnoreSource(nameof(User.Id))]
    public static partial UserDetailsGetResponse? ToDto(User? entity);
    


    [MapperIgnoreTarget(nameof(User.Id))]
    [MapperIgnoreTarget(nameof(User.ProfileImageId))]
    [MapperIgnoreTarget(nameof(User.ExternalId))]
    [MapperIgnoreTarget(nameof(User.MembershipHistory))]
    [MapperIgnoreTarget(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(UserCreateRequest.Password))]
    [MapperIgnoreSource(nameof(UserCreateRequest.MembershipHistory))]
    [MapperIgnoreSource(nameof(UserCreateRequest.RoleHistory))]
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

    private static partial UserDetailsGetResponseRole MapToUserDetailsGetResponseRole(Role role);
    
    [MapperIgnoreSource(nameof(UserMembershipHistory.UserId))]
    [MapperIgnoreSource(nameof(UserMembershipHistory.MembershipId))]
    [MapperIgnoreSource(nameof(UserMembershipHistory.Id))]
    private static partial UserDetailsGetResponseUserMembershipHistory MapToUserDetailsGetResponseUserMembershipHistory(UserMembershipHistory entity);

    [MapperIgnoreSource(nameof(User.ExternalId))]
    [MapperIgnoreSource(nameof(User.RoleHistory))]
    [MapperIgnoreSource(nameof(User.MembershipHistory))]
    [MapperIgnoreSource(nameof(User.Abouth))]
    [MapperIgnoreSource(nameof(User.ProfileImageId))]
    [MapperIgnoreSource(nameof(User.PreferNicknameOverName))]
    [MapperIgnoreTarget(nameof(UserPaginationGetResponse.Status))]
    [MapperIgnoreTarget(nameof(UserPaginationGetResponse.Roles))]
    private static partial UserPaginationGetResponse MapToPaginationDto(User entity);
}