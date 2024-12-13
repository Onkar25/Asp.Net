using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenServices(IConfiguration config) : ITokenServices
{
    public string CreateToken(AppUser user)
    {
        // Create Token key 
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenkey from appsetting");
        if (tokenKey.Length < 64) throw new Exception("Your tokenkey needs to be longer");

        // Create key for the Token Key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Create Claims for the token
        var claims = new List<Claim>
        { 
            new (ClaimTypes.NameIdentifier ,user.UserName),
        };

        // Create Credentials for the token
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Create Token Description
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = cred,
        };

        //Create Token Handle 
        var tokenHandler = new JwtSecurityTokenHandler();

        // Create token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // return the token
        return tokenHandler.WriteToken(token);
    }
}
