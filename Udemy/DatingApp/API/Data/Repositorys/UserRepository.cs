using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositorys;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
  public async Task<IEnumerable<MemberDto>> GetMemberAsyn()
  {
    return await context.Users
        .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        .ToListAsync();
  }

  public async Task<MemberDto?> GetMemberAsync(string username)
  {
    return await context.Users
          .Where(x => x.UserName == username)
          .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
          .SingleOrDefaultAsync();
  }

  public async Task<AppUsers?> GetUserByIdAsync(int id)
  {
    return await context.Users.FindAsync(id);
  }

  public async Task<AppUsers?> GetUserByUsernameAsync(string username)
  {
    return await context.Users
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username.ToLower());
  }

  public async Task<IEnumerable<AppUsers>> GetUsersAsync()
  {
    return await context.Users
                  .Include(x => x.Photos)
                  .ToListAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await context.SaveChangesAsync() > 0;
  }

  public void Update(AppUsers user)
  {
    context.Entry(user).State = EntityState.Modified;

  }
}
