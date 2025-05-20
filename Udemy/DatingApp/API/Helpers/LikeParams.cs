namespace API.Helpers;

public class LikeParams : PaginationParams
{
  public int UserId { get; set; }

  public required string Predicate { get; set; } = "liked";
}
