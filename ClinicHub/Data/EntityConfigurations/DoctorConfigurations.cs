namespace ClinicHub.Data.EntityConfigurations;

public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
{
	public void Configure(EntityTypeBuilder<Doctor> builder)
	{
		builder.Property(x => x.Name).HasMaxLength(100);
		builder.Property(x => x.MobileNumber).HasMaxLength(20);
		builder.Property(x => x.Address).HasMaxLength(100);
		builder.Property(e => e.NationalId).HasMaxLength(20);
		builder.HasIndex(x => x.MobileNumber).IsUnique();
		builder.HasIndex(x => x.Email).IsUnique();
	}
}