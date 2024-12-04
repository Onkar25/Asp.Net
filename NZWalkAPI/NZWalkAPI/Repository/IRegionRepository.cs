using System;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
	public interface IRegionRepository
	{
        Task<List<Region>> GetRegionsAsync();

        Task<Region?> GetRegionsByIdAsync(Guid id);

        Task InsertRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid id, Region region);

        Task<Region?> DeleteRegionAsync(Guid id);
    }
}

