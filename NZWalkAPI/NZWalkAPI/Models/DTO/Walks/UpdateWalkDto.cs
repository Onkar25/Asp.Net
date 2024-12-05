
using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTO.Walks
{
	public class UpdateWalkDto
	{
        [Required]
        [MaxLength(100, ErrorMessage = "Name maximum code lenght is 100 character")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Desription maximum code lenght is 100 character")]
        public string Desription { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Length should be between 1 - 100 km")]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}

