using System;
using System.Collections.Generic;

namespace PRACT1718.ModelsBD;

public partial class User
{
    public int UserId { get; set; }

    public string? UserSurName { get; set; }

    public string? UserName { get; set; }

    public string UserPatronymic { get; set; } = null!;

    public string? UserLogin { get; set; }

    public string? UserPassword { get; set; }

    public int? UserRole { get; set; }

    public virtual Role? UserRoleNavigation { get; set; }
}
