namespace API.Entities;

public class UserLike
{
  public AppUsers SourceUser { get; set; } = null!;
  public int SourceUserId { get; set; }
  public AppUsers TargetUser { get; set; } = null!;
  public int TargetUserId { get; set; }

}
