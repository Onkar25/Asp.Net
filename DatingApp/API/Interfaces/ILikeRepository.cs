using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface ILikeRepository
{
  Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId);
  Task<PagedList<MemberDto>> GetUserLikes(LikeParams likeParams);
  Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId);
  void DeleteLike(UserLike like);
  void AddLike(UserLike like);
}
