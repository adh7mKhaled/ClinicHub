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

	public IEnumerable<Patient> GetAll() => _context.Patients.ToList();

	public Patient? GetById(int id) => _context.Patients.Find(id);

	public bool Edit(Patient patient)
	{
		if (patient is null)
			return false;

		_context.Patients.Update(patient);
		_context.SaveChanges();

		return true;
	}
}