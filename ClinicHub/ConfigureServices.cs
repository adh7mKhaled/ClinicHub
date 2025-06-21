using ClinicHub.Core.Mapping;
using ClinicHub.Data;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace ClinicHub;

public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, WebApplicationBuilder builder)
	{
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(connectionString));

		services.AddDatabaseDeveloperPageExceptionFilter();

		services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
			.AddEntityFrameworkStores<ApplicationDbContext>();

		services.AddControllersWithViews();

		services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
		services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

		return services;
	}
}