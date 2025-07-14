namespace ClinicHub.Data.EntityConfigurations;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.HasIndex(x => x.UserName).IsUnique();
		builder.Property(x => x.FullName).HasMaxLength(100);
		builder.HasIndex(x => x.Email).IsUnique();
	}
}