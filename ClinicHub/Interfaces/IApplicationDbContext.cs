namespace ClinicHub.Interfaces;

public interface IApplicationDbContext
{
	DbSet<Patient> Patients { get; set; }
	DbSet<Specialty> Specialties { get; set; }

	int SaveChanges();
}