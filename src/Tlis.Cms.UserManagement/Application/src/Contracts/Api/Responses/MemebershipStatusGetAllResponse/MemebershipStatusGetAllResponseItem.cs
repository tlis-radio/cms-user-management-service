using System;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class MembershipStatusGetAllResponseItem
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}