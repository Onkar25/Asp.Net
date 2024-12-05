using System;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
	public class SqlWalkRepository : IWalkRepository
	{
        private readonly NZWalkDbContext dbContext;

        public SqlWalkRepository(NZWalkDbContext dbContext)
		{
            this.dbContext = dbContext;
        }

        public async Task<Walk?> DeleteWalk(Guid id)
        {

            var walkDomainData = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomainData == null)
                return null;

            dbContext.Walks.Remove(walkDomainData);
            await dbContext.SaveChangesAsync();

            return walkDomainData;
        }

        public async Task<List<Walk>> GetAllWalks()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public Task<Walk?> GetWalkById(Guid id)
        {
            return dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> InsertWalkData(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateWalk(Guid id, Walk walk)
        {
            var walkDomainData = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomainData == null)
                return null;

            walkDomainData.Name = walk.Name;
            walkDomainData.Desription = walk.Desription;
            walkDomainData.LengthInKm = walk.LengthInKm;
            walkDomainData.WalkImageUrl = walk.WalkImageUrl;
            walkDomainData.DifficultyId = walk.DifficultyId;
            walkDomainData.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return walkDomainData;
        }
    }
}

