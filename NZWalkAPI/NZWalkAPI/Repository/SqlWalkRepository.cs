using System;
using System.Linq;
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

        public async Task<List<Walk>> GetAllWalks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNo = 1, int pageSize = 100)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                if (sortBy.Equals("length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            var skipWalks = (pageNo - 1) * pageSize;

            return await walks.Skip(skipWalks).Take(pageSize).ToListAsync();
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

