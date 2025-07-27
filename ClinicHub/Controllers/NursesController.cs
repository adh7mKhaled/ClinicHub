namespace ClinicHub.Controllers;

public class NursesController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<NurseFormViewModel> validator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<NurseFormViewModel> _validator = validator;

	public IActionResult Index()
	{
		var viewModelv = _mapper.Map<IEnumerable<NurseViewModel>>(_unitOfWork.Nurses.GetAll());
		return View(viewModelv);
	}

	public IActionResult Create()
	{
		NurseFormViewModel viewModel = new()
		{
			Doctors = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Doctors.GetAll())
		};
		return View("_Form", viewModel);
	}

	[HttpPost]
	public IActionResult Create(NurseFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var nurse = _mapper.Map<Nurse>(model);

		nurse.CreatedById = User.GetUserId();
		nurse.CreatedOn = DateTime.Now;

		_unitOfWork.Nurses.Add(nurse);
		_unitOfWork.Complete();

		return RedirectToAction(nameof(Index));
	}

}