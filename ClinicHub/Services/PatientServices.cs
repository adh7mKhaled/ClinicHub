namespace ClinicHub.Services;

public class PatientServices : IPatientServices
{
	private readonly IApplicationDbContext _context;

	public PatientServices(IApplicationDbContext context)
	{
		_context = context;
	}

	public void Add(Patient patient) => _context.Patients.Add(patient);
	public IEnumerable<Patient> GetAll() => _context.Patients.ToList();
	public int Save() => _context.SaveChanges();
}