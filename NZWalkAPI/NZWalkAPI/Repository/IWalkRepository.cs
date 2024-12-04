using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
    public interface IWalkRepository
	{
        Task<Walk> InsertWalkData(Walk walk);
    }
}

