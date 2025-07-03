using System.Linq.Expressions;

namespace ClinicHub.Services;

public interface IPatientServices
{
	IEnumerable<Patient> GetAll();
	void Add(Patient patient);
	Patient? GetById(int id);
	bool Edit(Patient patient);
	public Patient Find(Expression<Func<Patient, bool>> predicate);
	int Save();
}