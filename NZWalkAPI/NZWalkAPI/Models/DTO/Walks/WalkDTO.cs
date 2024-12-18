﻿using System;
using NZWalkAPI.Models.DTO.Difficulty;

namespace NZWalkAPI.Models.DTO.Walks
{
	public class WalkDTO
	{
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Desription { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public RegionDTO Region { get; set; }

        public DifficultyDto Difficulty { get; set; }
    }
}

