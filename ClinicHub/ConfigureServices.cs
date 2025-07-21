using ClinicHub.Core.Mapping;
using ClinicHub.Data;
using ClinicHub.Data.UnitOfWork;
using ClinicHub.Helpers;
using ClinicHub.Services;
using FluentValidation.AspNetCore;
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

		services.Configure<IdentityOptions>(options =>
		{
			options.Password.RequiredLength = 8;
		});

		services.AddControllersWithViews();

		// Auto Validate Anti Forgery Token
		services.AddMvc(options =>
			options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
		);

		services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IUniquenessValidator, UniquenessValidator>();

		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddFluentValidationAutoValidation();
		services.AddFluentValidationClientsideAdapters();

		services.AddExpressiveAnnotations();

		services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

		return services;
	}
}