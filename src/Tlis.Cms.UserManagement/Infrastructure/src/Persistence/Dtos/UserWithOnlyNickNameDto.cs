using System;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Dtos;

public class UserWithOnlyNicknameDto
{
    public Guid Id { get; set; }

    public string Nickname { get; set; } = null!;
}