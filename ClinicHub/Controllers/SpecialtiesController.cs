using ClinicHub.Services.Specialties;

namespace ClinicHub.Controllers;

public class SpecialtiesController(ISpecialtyServices specialtyServices, IMapper mapper) : Controller
{
	private readonly ISpecialtyServices _specialtyServices = specialtyServices;
	private readonly IMapper _mapper = mapper;

	public IActionResult Index()
	{
		var specialties = _specialtyServices.GetAll();
		return View(_mapper.Map<IEnumerable<SpecialtyViewModel>>(specialties));
	}
}