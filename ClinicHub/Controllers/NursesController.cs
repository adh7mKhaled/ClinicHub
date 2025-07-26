namespace ClinicHub.Controllers;

public class NursesController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;

	public IActionResult Index()
	{
		var viewModelv = _mapper.Map<IEnumerable<NurseViewModel>>(_unitOfWork.Nurses.GetAll());
		return View(viewModelv);
	}
}