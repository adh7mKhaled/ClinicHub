namespace ClinicHub.Services.Specialties;

public interface ISpecialtyServices
{
	IEnumerable<Specialty> GetAll();
	void Add(Specialty specialty);
}