using System;
using AutoMapper;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;
using NZWalkAPI.Models.DTO.Walks;

namespace NZWalkAPI.Mappings
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionDTO>().ReverseMap();
			CreateMap<Walk, AddWalkRequestDto>().ReverseMap();
            CreateMap<WalkDTO, Walk>().ReverseMap();
        }
	}
}

