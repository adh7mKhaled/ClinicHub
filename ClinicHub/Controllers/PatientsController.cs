namespace ClinicHub.Controllers;

public class PatientsController : Controller
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public PatientsController(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public IActionResult Index()
	{
		var viewModel = _mapper.Map<IEnumerable<PatientViewModel>>(_context.Patients.ToList());
		return View(viewModel);
	}
}