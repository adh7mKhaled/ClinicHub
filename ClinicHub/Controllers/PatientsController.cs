namespace ClinicHub.Controllers;

public class PatientsController : Controller
{
	private readonly IPatientServices _patientServices;
	private readonly IMapper _mapper;
	private readonly IValidator<PatientFormViewModel> _validator;

	public PatientsController(IPatientServices patientServices, IMapper mapper, IValidator<PatientFormViewModel> validator)
	{
		_patientServices = patientServices;
		_mapper = mapper;
		_validator = validator;
	}

	public IActionResult Index()
	{
		var patients = _patientServices.GetAll();
		var viewModel = _mapper.Map<IEnumerable<PatientViewModel>>(patients);

		return View(viewModel);
	}

	public IActionResult Create()
	{
		return View("_Form");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(PatientFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var patient = _mapper.Map<Patient>(model);

		_patientServices.Add(patient);

		return RedirectToAction(nameof(Index));
	}

	public IActionResult Edit(int Id)
	{
		var patient = _patientServices.GetById(Id);

		if (patient is null)
			return NotFound();

		return View("_Form", _mapper.Map<PatientFormViewModel>(patient));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Edit(PatientFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var patient = _mapper.Map<Patient>(model);

		patient.LastUpdatedOn = DateTime.Now;

		_patientServices.Edit(patient);

		return RedirectToAction(nameof(Index));
	}

	public IActionResult Details(int id)
	{
		var patient = _patientServices.GetById(id);

		if (patient is null)
			return NotFound();

		var viewModel = _mapper.Map<PatientViewModel>(patient);

		return View(viewModel);
	}

	public IActionResult ToggleStatus(int id)
	{
		var patient = _patientServices.GetById(id);

		if (patient is null)
			return BadRequest();

		patient.IsDeleted = !patient.IsDeleted;
		patient.LastUpdatedOn = DateTime.Now;
		_patientServices.Save();

		return Ok(patient.LastUpdatedOn.ToString());
	}
}