using ClinicHub.Core.Consts;

namespace ClinicHub.Seeds;

public static class DefaultUsers
{
	public static async Task SeddAdminUserAsync(UserManager<ApplicationUser> userManager)
	{
		ApplicationUser admin = new()
		{
			UserName = "admin",
			Email = "adhammkhaledd.99@gmail.com",
			FullName = "adham khaled",
			EmailConfirmed = true,
		};

		var user = await userManager.FindByEmailAsync(admin.Email);

		if (user is null)
		{
			await userManager.CreateAsync(admin, "0882251@MOh#");
			await userManager.AddToRoleAsync(admin, AppRoles.Admin);
		}

	}
}