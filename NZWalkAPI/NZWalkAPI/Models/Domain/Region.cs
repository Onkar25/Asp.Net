using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.Domain
{
	public class Region
	{
		public Guid Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Code maximum code lenght is 3 character")]
        [MaxLength(3, ErrorMessage = "Code maximum code lenght is 3 character")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name maximum code lenght is 100 character")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}

