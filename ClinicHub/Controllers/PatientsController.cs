using System.Security.Claims;

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
		return PartialView("_Form");
	}

	[HttpPost]
	public IActionResult Create(PatientFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var patient = _mapper.Map<Patient>(model);

		patient.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

		_patientServices.Add(patient);

		return Ok();
	}

	public IActionResult Edit(int Id)
	{
		var patient = _patientServices.GetById(Id);

		if (patient is null)
			return NotFound();

		return PartialView("_Form", _mapper.Map<PatientFormViewModel>(patient));
	}

	[HttpPost]
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

		return Ok();
	}
	public IActionResult AllowUniqueMobileNumber(PatientFormViewModel model)
	{
		var patient = _patientServices.Find(p => p.MobileNumber == model.MobileNumber);

		var isAllowed = patient is null || patient.Id.Equals(model.Id);

		return Json(isAllowed);
	}

	public IActionResult AllowUniqueEmail(PatientFormViewModel model)
	{
		var patient = _patientServices.Find(p => p.Email == model.Email);

		var isAllowed = patient is null || patient.Id.Equals(model.Id);

		return Json(isAllowed);
	}
}