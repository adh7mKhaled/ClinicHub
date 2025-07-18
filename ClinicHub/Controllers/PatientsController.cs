using ClinicHub.Data.UnitOfWork;

namespace ClinicHub.Controllers;

public class PatientsController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<PatientFormViewModel> validator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<PatientFormViewModel> _validator = validator;

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
		var patient = _unitOfWork.Patients.Find(p => p.MobileNumber == model.MobileNumber);

		var isAllowed = patient is null || patient.Id.Equals(model.Id);

		return Json(isAllowed);
	}

	public IActionResult AllowUniqueEmail(PatientFormViewModel model)
	{
		var patient = _unitOfWork.Patients.Find(p => p.Email == model.Email);

		var isAllowed = patient is null || patient.Id.Equals(model.Id);

		return Json(isAllowed);
	}
}