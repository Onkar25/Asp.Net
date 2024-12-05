using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTO
{
	public class AddRegionDTO
	{
        [Required]
        [MinLength(3,ErrorMessage = "Code maximum code lenght is 3 character")]
        [MaxLength(3 , ErrorMessage = "Code maximum code lenght is 3 character")]
        public string Code { get; set; }

        [Required(ErrorMessage ="Name is Manditory field")]
        [MaxLength(100, ErrorMessage = "Name maximum code lenght is 100 character")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}

