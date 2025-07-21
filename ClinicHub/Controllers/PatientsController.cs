using ClinicHub.Data.UnitOfWork;

namespace ClinicHub.Controllers;

public class PatientsController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<PatientFormViewModel> validator, IUniquenessValidator uniquenessValidator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<PatientFormViewModel> _validator = validator;
	private readonly IUniquenessValidator _uniquenessValidator = uniquenessValidator;

	public IActionResult Index()
	{
		var patients = _unitOfWork.Patients.GetAll();

		return View(_mapper.Map<IEnumerable<PatientViewModel>>(patients));
	}

	[AjaxOnly]
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

		patient.CreatedById = User.GetUserId();

		_unitOfWork.Patients.Add(patient);
		_unitOfWork.Complete();

		return PartialView("_patientRow", _mapper.Map<PatientViewModel>(patient));
	}

	[AjaxOnly]
	public IActionResult Edit(int Id)
	{
		var patient = _unitOfWork.Patients.GetById(Id);

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
		patient.LastUpdatedById = User.GetUserId();

		_unitOfWork.Patients.Update(patient);
		_unitOfWork.Complete();

		return PartialView("_patientRow", _mapper.Map<PatientViewModel>(patient));
	}

	public IActionResult Details(int id)
	{
		var patient = _unitOfWork.Patients.GetById(id);

		return patient is null ? NotFound() : View(_mapper.Map<PatientViewModel>(patient));
	}

	public IActionResult ToggleStatus(int id)
	{
		var patient = _unitOfWork.Patients.GetById(id);

		if (patient is null)
			return BadRequest();

		patient.IsDeleted = !patient.IsDeleted;
		patient.LastUpdatedOn = DateTime.Now;
		_unitOfWork.Complete();

		return Ok();
	}
	public IActionResult AllowUniqueMobileNumber(PatientFormViewModel model)
	{
		return _uniquenessValidator.IsUnique(_unitOfWork.Patients, p => p.MobileNumber == model.MobileNumber, model.Id, p => p.Id);
	}

	public IActionResult AllowUniqueEmail(PatientFormViewModel model)
	{
		return _uniquenessValidator.IsUnique(_unitOfWork.Patients, p => p.Email == model.Email, model.Id, p => p.Id);
	}
}