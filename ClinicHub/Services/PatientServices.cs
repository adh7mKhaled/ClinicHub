namespace ClinicHub.Services;

public class PatientServices : IPatientServices
{
	private readonly IApplicationDbContext _context;

	public PatientServices(IApplicationDbContext context)
	{
		_context = context;
	}

	public IEnumerable<Patient> GetAll()
	{
		return _context.Patients.ToList();
	}
}