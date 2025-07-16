using ClinicHub.Data.Repositories;

namespace ClinicHub.Data.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
	private readonly ApplicationDbContext _context = context;

	public IBaseRepository<Patient> Patients => new BaseRepository<Patient>(_context);

	public IBaseRepository<Specialty> Specialties => new BaseRepository<Specialty>(_context);

	public int Complete()
	{
		return _context.SaveChanges();
	}
}