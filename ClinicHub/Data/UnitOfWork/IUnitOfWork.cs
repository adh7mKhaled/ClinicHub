using ClinicHub.Data.Repositories;

namespace ClinicHub.Data.UnitOfWork;

public interface IUnitOfWork
{
	IBaseRepository<Patient> Patients { get; }
	IBaseRepository<Specialty> Specialties { get; }
	IBaseRepository<Doctor> Doctors { get; }
	IBaseRepository<Nurse> Nurses { get; }
	IBaseRepository<DoctorSchedule> DoctorSchedules { get; }
	IBaseRepository<Appointment> Appointments { get; }

	int Complete();
}