using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTO.Authentication
{
    public class RegisterUserDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string[] Roles { get; set; }
	}
}

