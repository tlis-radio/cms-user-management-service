using Riok.Mapperly.Abstractions;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Application.Mappers;

[Mapper]
internal static partial class RoleMapper
{
    public static partial RoleGetAllResponseItem ToRoleGetAllResponseItem(Role dto);
}