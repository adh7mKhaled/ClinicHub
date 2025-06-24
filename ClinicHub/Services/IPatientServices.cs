namespace ClinicHub.Services;

public interface IPatientServices
{
	IEnumerable<Patient> GetAll();
	void Add(Patient patient);
	int Save();
}