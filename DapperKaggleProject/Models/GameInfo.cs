using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class GameInfo
{
    public string GameId { get; set; } = null!;

    public DateOnly? GameDate { get; set; }

    public int? Attendance { get; set; }

    public string? GameTime { get; set; }

    public virtual Game Game { get; set; } = null!;
}
