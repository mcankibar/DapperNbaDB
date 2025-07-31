using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class Player
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public int IsActive { get; set; }

    public virtual CommonPlayerInfo? CommonPlayerInfo { get; set; }
}
