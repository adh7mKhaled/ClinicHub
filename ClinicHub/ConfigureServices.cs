using ClinicHub.Core.Mapping;
using ClinicHub.Data;
using ClinicHub.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;

namespace ClinicHub;

public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, WebApplicationBuilder builder)
	{
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(connectionString));

		services.AddDatabaseDeveloperPageExceptionFilter();

		services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultUI()
			.AddDefaultTokenProviders();

		services.AddControllersWithViews();

		services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
		services.AddScoped<IPatientServices, PatientServices>();

		services.AddScoped<IValidator<PatientFormViewModel>, PatientFormValidator>();
		services.AddFluentValidationAutoValidation();
		services.AddFluentValidationClientsideAdapters();

		services.AddExpressiveAnnotations();

		services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

		return services;
	}
}