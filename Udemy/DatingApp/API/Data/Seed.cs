using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
  public static async Task SeedUser(UserManager<AppUsers> userManager, RoleManager<AppRole> roleManager)
  {
    if (await userManager.Users.AnyAsync())
      return;

    var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

    var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var users = JsonSerializer.Deserialize<List<AppUsers>>(userData, option);

    if (users == null)
      return;

    var roles = new List<AppRole>
    {
      new() {Name = "Member"},
      new() {Name = "Admin"},
      new() {Name = "Moderator"},
    };
    foreach (var role in roles)
    {
      await roleManager.CreateAsync(role);
    }
    foreach (var user in users)
    {
      // using var hmac = new HMACSHA512();
      // user.UserName = user.UserName.ToLower();
      // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
      // user.PasswordSalt = hmac.Key;
      // context.Users.Add(user);
      user.UserName = user.UserName!.ToLower();
      await userManager.CreateAsync(user, "Pa$$w0rd");
      await userManager.AddToRoleAsync(user, "Member");
    }

    var admin = new AppUsers
    {
      UserName = "admin",
      KnownAs = "admin",
      Gender = "",
      City = "",
      Country = "",
    };

    await userManager.CreateAsync(admin, "Pa$$w0rd");
    await userManager.AddToRolesAsync(admin, ["Admin", "Moderator"]);
    // await context.SaveChangesAsync();
  }
}
