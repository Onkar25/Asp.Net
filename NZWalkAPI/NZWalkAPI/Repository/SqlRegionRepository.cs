using System;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;

namespace NZWalkAPI.Repository
{
	public class SqlRegionRepository : IRegionRepository
	{
        private readonly NZWalkDbContext DbContext;

        public SqlRegionRepository(NZWalkDbContext dbContext)
		{
            DbContext = dbContext;
        }
        
        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var regionDomain = await DbContext.Regions.FirstOrDefaultAsync(reg => reg.Id == id);
            if(regionDomain == null)
            {
                return null;
            }
            DbContext.Regions.Remove(regionDomain);
            await DbContext.SaveChangesAsync();
            return regionDomain;
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
           return await DbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionsByIdAsync(Guid id)
        {
            var regionDomain = await DbContext.Regions.FirstOrDefaultAsync(reg => reg.Id == id);
            if (regionDomain == null)
            {
                return null;
            }
            return regionDomain;
        }

        public async Task InsertRegionAsync(Region region)
        {
            await DbContext.Regions.AddAsync(region);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var regionDomain = await DbContext.Regions.FirstOrDefaultAsync(reg => reg.Id == id);
            if (regionDomain == null)
            {
                return null;
            }
            regionDomain.Code = region.Code;
            regionDomain.Name = region.Name;
            regionDomain.RegionImageUrl = region.RegionImageUrl;

            await DbContext.SaveChangesAsync();

            return regionDomain;
        }
    }
}

