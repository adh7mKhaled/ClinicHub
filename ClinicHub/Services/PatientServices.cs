using System.Linq.Expressions;

namespace ClinicHub.Services;

public class PatientServices : IPatientServices
{
	private readonly IApplicationDbContext _context;

	public PatientServices(IApplicationDbContext context)
	{
		_context = context;
	}

	public void Add(Patient patient)
	{
		_context.Patients.Add(patient);
		_context.SaveChanges();
	}

	public IEnumerable<Patient> GetAll() => _context.Patients.Where(p => !p.IsDeleted).ToList();

	public Patient? GetById(int id) => _context.Patients.Find(id);

	public bool Edit(Patient patient)
	{
		if (patient is null)
			return false;

		_context.Patients.Update(patient);
		_context.SaveChanges();

		return true;
	}
	public int Save() => _context.SaveChanges();

	public Patient Find(Expression<Func<Patient, bool>> predicate) =>
		_context.Patients.SingleOrDefault(predicate);
}