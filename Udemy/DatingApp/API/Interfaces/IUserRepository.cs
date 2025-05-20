using API.DTOs;
using API.Entities;
using API.Helpers;
namespace API.Interfaces;

public interface IUserRepository
{
  void Update(AppUsers user);
  Task<bool> SaveAllAsync();
  Task<IEnumerable<AppUsers>> GetUsersAsync();
  Task<AppUsers?> GetUserByIdAsync(int id);
  Task<AppUsers?> GetUserByUsernameAsync(string username);
  Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
  Task<MemberDto?> GetMemberAsync(string username);
}
