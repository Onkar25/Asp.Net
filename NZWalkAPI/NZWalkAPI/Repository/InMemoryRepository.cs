using System;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
	public class InMemoryRepository //: IRegionRepository
    {

        public Task<Region?> DeleteRegionAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
            return new List<Region> { new Region {Id = Guid.NewGuid() , Code = "360" , Name = "Crooks" , RegionImageUrl = "image.png"} };
        }

        public Task<Region?> GetRegionsByIdAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task InsertRegionAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRegionAsync(Guid guid, Region region)
        {
            throw new NotImplementedException();
        }
    }
}

