namespace ClinicHub.Interfaces;

public interface IApplicationDbContext
{
	DbSet<Patient> Patients { get; set; }

	int SaveChanges();
}