using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicHub.Data.EntityConfigurations;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.HasIndex(x => x.UserName).IsUnique();
		builder.HasIndex(x => x.UserName).IsUnique();
		builder.HasIndex(x => x.Email).IsUnique();
	}
}
