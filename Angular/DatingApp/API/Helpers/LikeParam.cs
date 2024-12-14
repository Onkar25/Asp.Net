using System;

namespace API.Helpers;

public class LikeParam : PaginationParam
{
public int UserID { get; set; }

public string Predicate { get; set; } = "liked";
}
