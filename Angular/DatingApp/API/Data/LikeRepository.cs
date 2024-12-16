using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class LikeRepository(DataContext context, IMapper mapper) : ILikeRepository
{
    public void AddLike(UserLike like)
    {
        context.Likes.Add(like);
    }

    public void DeleteLike(UserLike like)
    {
        context.Likes.Remove(like);
    }

    public async Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId)
    {
        return await context.Likes.Where(x => x.SourceUserId == currentUserId).Select(x => x.TargetUserId).ToListAsync();
    }

    public async Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId)
    {
        return await context.Likes.FindAsync(sourceUserId, targetUserId);
    }

    public async Task<PagedList<MembersDto>> GetUserLikes(LikeParam param)
    {
        var likes = context.Likes.AsQueryable();
        IQueryable<MembersDto> query;
        switch (param.Predicate)
        {
            case "liked":
                query = likes
                .Where(x => x.SourceUserId == param.UserID)
                .Select(x => x.TargetUser)
                .ProjectTo<MembersDto>(mapper.ConfigurationProvider);
                break;
            case "likedBy":
                query = likes
               .Where(x => x.TargetUserId == param.UserID)
               .Select(x => x.SourceUser)
               .ProjectTo<MembersDto>(mapper.ConfigurationProvider);
                break;
            default:
                var likesId = await GetCurrentUserLikeIds(param.UserID);
                query = likes
                .Where(x => x.TargetUserId == param.UserID && likesId.Contains(x.SourceUserId))
                .Select(x => x.SourceUser)
                .ProjectTo<MembersDto>(mapper.ConfigurationProvider);
                break;
        }

        return await PagedList<MembersDto>.CreateAsync(query, param.pageNumber, param.PageSize);
    }

    public async Task<bool> SaveChanges()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
