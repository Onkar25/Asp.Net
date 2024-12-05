using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.DTO.Authentication;
using NZWalkAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        // GET: api/values
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerUserDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerUserDto.Roles != null && registerUserDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerUserDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registered Successfully !!! ");
                    }
                }
            }

            return BadRequest("Something went wrong !!!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var userResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (userResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null && roles.Any())
                    {
                        var jwtToken = tokenRepository.GenerateJwtToken(user, roles.ToList());
                        if (!string.IsNullOrWhiteSpace(jwtToken))
                        {
                            var loginResponse = new LoginResponseDto
                            {
                                JwtToken = jwtToken
                            };

                            return Ok(loginResponse);
                        }
                    }
                }
            }
            return BadRequest("Username & Password is incorrect !!!");
        }

    }
}

