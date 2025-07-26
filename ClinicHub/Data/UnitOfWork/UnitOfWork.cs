using ClinicHub.Data.Repositories;

namespace ClinicHub.Data.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
	private readonly ApplicationDbContext _context = context;

	public IBaseRepository<Patient> Patients => new BaseRepository<Patient>(_context);

	public IBaseRepository<Specialty> Specialties => new BaseRepository<Specialty>(_context);

	public IBaseRepository<Doctor> Doctors => new BaseRepository<Doctor>(_context);

	public IBaseRepository<Nurse> Nurses => new BaseRepository<Nurse>(_context);

	public int Complete()
	{
		return _context.SaveChanges();
	}
}