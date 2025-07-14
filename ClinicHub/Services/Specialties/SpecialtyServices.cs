namespace ClinicHub.Services.Specialties;

public class SpecialtyServices(IApplicationDbContext context) : ISpecialtyServices
{
	private readonly IApplicationDbContext _context = context;

	public IEnumerable<Specialty> GetAll() => _context.Specialties.Where(p => !p.IsDeleted).ToList();
}