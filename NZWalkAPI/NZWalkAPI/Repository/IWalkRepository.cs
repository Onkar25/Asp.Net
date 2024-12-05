using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> InsertWalkData(Walk walk);

        Task<List<Walk>> GetAllWalks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNo = 1, int pageSize = 100);

        Task<Walk?> GetWalkById(Guid id);

        Task<Walk?> UpdateWalk(Guid id, Walk walk);

        Task<Walk?> DeleteWalk(Guid id);
    }
}

