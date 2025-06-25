namespace ClinicHub.Services;

public interface IPatientServices
{
	IEnumerable<Patient> GetAll();
	void Add(Patient patient);
	Patient? GetById(int id);
	bool Edit(Patient patient);
}