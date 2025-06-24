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
	public IActionResult Create(PatientFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest(validationResult);

		var patient = _mapper.Map<Patient>(model);

		_patientServices.Add(patient);
		_patientServices.Save();

		return RedirectToAction(nameof(Index));
	}
}