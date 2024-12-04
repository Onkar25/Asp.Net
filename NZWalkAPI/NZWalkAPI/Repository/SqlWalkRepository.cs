using System;
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

        public async Task<Walk> InsertWalkData(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
    }
}

