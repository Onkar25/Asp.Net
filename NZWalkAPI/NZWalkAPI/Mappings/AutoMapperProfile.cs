using System;
using AutoMapper;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;

namespace NZWalkAPI.Mappings
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO>().ReverseMap();
        }
	}
}

