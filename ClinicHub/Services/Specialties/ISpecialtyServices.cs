namespace ClinicHub.Services.Specialties;

public interface ISpecialtyServices
{
	IEnumerable<Specialty> GetAll();
	void Add(Specialty specialty);
	Specialty? GetById(int id);
	bool Edit(Specialty specialty);
	Specialty Find(Expression<Func<Specialty, bool>> predicate);
}