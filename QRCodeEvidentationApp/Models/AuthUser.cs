using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class AuthUser
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }
}
