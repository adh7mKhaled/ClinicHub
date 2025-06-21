namespace ClinicHub.Services;

public interface IPatientServices
{
	IEnumerable<Patient> GetAll();
}
