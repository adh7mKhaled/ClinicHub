namespace ClinicHub.Services.Specialties;

public class SpecialtyServices(IApplicationDbContext context) : ISpecialtyServices
{
	private readonly IApplicationDbContext _context = context;

	public IEnumerable<Specialty> GetAll() => _context.Specialties.Where(p => !p.IsDeleted).ToList();

	public void Add(Specialty specialty)
	{
		_context.Specialties.Add(specialty);
		_context.SaveChanges();
	}

	public Specialty? GetById(int id) => _context.Specialties.Find(id);

	public bool Edit(Specialty specialty)
	{
		if (specialty is null)
			return false;

		_context.Specialties.Update(specialty);
		_context.SaveChanges();

		return true;
	}
}