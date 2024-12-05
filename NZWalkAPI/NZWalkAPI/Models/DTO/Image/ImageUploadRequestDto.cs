using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTO.Image
{
	public class ImageUploadRequestDto
	{
        [Required]
        public IFormFile File { get; set; }

		[Required]
		public string Filename { get; set; }

		public string? FileDescription { get; set; }
	}
}

