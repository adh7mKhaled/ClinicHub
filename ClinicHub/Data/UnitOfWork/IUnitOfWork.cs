using ClinicHub.Data.Repositories;

namespace ClinicHub.Data.UnitOfWork;

public interface IUnitOfWork
{
	IBaseRepository<Patient> Patients { get; }
	IBaseRepository<Specialty> Specialties { get; }
	IBaseRepository<Doctor> Doctors { get; }

	int Complete();
}