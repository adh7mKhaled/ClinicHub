using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicHub.Data.EntityConfigurations;

public class PatientConfigurations : IEntityTypeConfiguration<Patient>
{
	public void Configure(EntityTypeBuilder<Patient> builder)
	{
		builder.HasIndex(x => x.MobileNumber).IsUnique();
		builder.HasIndex(x => x.Email).IsUnique();
		builder.Property(x => x.Name).HasMaxLength(100);
		builder.Property(x => x.Notes).HasMaxLength(1500);
		builder.Property(x => x.MobileNumber).HasMaxLength(20);
		builder.Property(x => x.City).HasMaxLength(50);
	}
}