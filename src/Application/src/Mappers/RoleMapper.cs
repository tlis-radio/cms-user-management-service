using Riok.Mapperly.Abstractions;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Application.Mappers;

[Mapper]
internal partial class RoleMapper
{
    [MapperIgnoreSource(nameof(Role.UserRoleHistory))]
    public partial RoleGetAllResponseItem ToRoleGetAllResponseItem(Role dto);
}