namespace ClinicHub.Data.EntityConfigurations;

public class SpecialtyConfigurations : IEntityTypeConfiguration<Specialty>
{
	public void Configure(EntityTypeBuilder<Specialty> builder)
	{
		builder.Property(x => x.Name).HasMaxLength(100);
		builder.Property(x => x.Description).HasMaxLength(500);
	}
}