using API.Entities;

namespace API.Interfaces;

public interface ITokenServices
{
    string CreateToken(AppUser user);
}
