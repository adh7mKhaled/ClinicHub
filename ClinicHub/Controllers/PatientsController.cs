namespace ClinicHub.Controllers;

public class PatientsController : Controller
{
	private readonly IApplicationDbContext _context;

	public PatientsController(IApplicationDbContext context)
	{
		_context = context;
	}

	public IActionResult Index()
	{

		return View();
	}
}