using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
    public interface IWalkRepository
	{
        Task<Walk> InsertWalkData(Walk walk);

        Task<List<Walk>> GetAllWalks();

        Task<Walk?> GetWalkById(Guid id);

        Task<Walk?> UpdateWalk(Guid id, Walk walk);

        Task<Walk?> DeleteWalk(Guid id);
    }
}

