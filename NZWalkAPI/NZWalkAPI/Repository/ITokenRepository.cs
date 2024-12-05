using System;
using Microsoft.AspNetCore.Identity;

namespace NZWalkAPI.Repository
{
	public interface ITokenRepository
	{
		string GenerateJwtToken(IdentityUser user, List<string> roles);
	}
}

