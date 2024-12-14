using System;

namespace API.Helpers;

public class UserParams : PaginationParam
{
    public string? Gender { get; set; }

    public string OrderBy { get; set; } = "lastActive";

    public string? CurrentUsername { get; set; }

    public int MinAge { get; set; } = 18;
    public int MaxAge { get; set; } = 100;

}
