namespace ClinicHub.Controllers;

public class PatientsController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<PatientFormViewModel> validator, IUniquenessValidator uniquenessValidator,
	IHashids hashids) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<PatientFormViewModel> _validator = validator;
	private readonly IUniquenessValidator _uniquenessValidator = uniquenessValidator;
	private readonly IHashids _hashids = hashids;

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Search(SearchFormViewModel model)
	{
		if (!ModelState.IsValid)
			return View(nameof(Index), model);

		var query = _unitOfWork.Patients.GetQueryable();

		var patient = query.SingleOrDefault(x => x.Email == model.Value || x.MobileNumber == model.Value);

		var viewModel = _mapper.ProjectTo<PatientSearchResultViewModel>(query)
			.SingleOrDefault(x => x.Email == model.Value || x.MobileNumber == model.Value);

		if (patient is not null)
			viewModel!.Key = _hashids.Encode(patient.Id);

		return PartialView("_Result", viewModel);
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

		return RedirectToAction(nameof(Details), new { key = _hashids.Encode(patient.Id) });
	}

	public IActionResult Edit(string key)
	{
		var patient = _unitOfWork.Patients.GetById(_hashids.Decode(key)[0]);

		return patient is null ? NotFound() : PartialView("_Form", _mapper.Map<PatientFormViewModel>(patient));
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

		return Ok();
	}

	public IActionResult Details(string key)
	{
		var patient = _unitOfWork.Patients.GetById(_hashids.Decode(key)[0]);

		return patient is null ? NotFound() : View(_mapper.Map<PatientViewModel>(patient));
	}

	public IActionResult ToggleStatus(int id)
	{
		if (_unitOfWork.Patients.GetById(id) is not { } patient)
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