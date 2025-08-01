using ClinicHub.Data.Repositories;

namespace ClinicHub.Controllers;

public class NursesController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<NurseFormViewModel> validator,
	IHashids hashids, IUniquenessValidator uniquenessValidator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<NurseFormViewModel> _validator = validator;
	private readonly IHashids _hashids = hashids;
	private readonly IUniquenessValidator _uniquenessValidator = uniquenessValidator;

	public IActionResult Index()
	{
		var nurses = _unitOfWork.Nurses.GetQueryable().Include(x => x.Doctor);
		var viewModelv = _mapper.Map<IEnumerable<NurseViewModel>>(nurses);

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

		return RedirectToAction(nameof(Details), new { key = _hashids.Encode(nurse.Id) });
	}

	public IActionResult Edit(string key)
	{
		var nurse = _unitOfWork.Nurses.GetById(_hashids.Decode(key)[0]);

		if (nurse is null)
			return NotFound();

		var viewModel = _mapper.Map<NurseFormViewModel>(nurse);
		viewModel.Doctors = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Doctors.GetAll());
		viewModel.Key = key;

		return View("_Form", viewModel);
	}

	[HttpPost]
	public IActionResult Edit(NurseFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var nurse = _mapper.Map<Nurse>(model);

		nurse.Id = _hashids.Decode(model.Key)[0];
		nurse.LastUpdatedById = User.GetUserId();
		nurse.LastUpdatedOn = DateTime.Now;

		_unitOfWork.Nurses.Update(nurse);
		_unitOfWork.Complete();

		return RedirectToAction(nameof(Details), new { key = model.Key });
	}

	public IActionResult Details(string key)
	{
		var nurse = _unitOfWork.Nurses.GetById(_hashids.Decode(key)[0]);

		if (nurse is null)
			return NotFound();

		nurse.Doctor = _unitOfWork.Doctors.GetById(nurse.DoctorId)!;

		return View(_mapper.Map<NurseViewModel>(nurse));
	}

	public IActionResult ToggleStatus(int id)
	{
		var patient = _unitOfWork.Nurses.GetById(id);

		if (patient is null)
			return BadRequest();

		patient.IsDeleted = !patient.IsDeleted;
		patient.LastUpdatedOn = DateTime.Now;
		_unitOfWork.Complete();

		return Ok();
	}

	private IActionResult CheckUnique<T>(string key, IBaseRepository<T> repository, Expression<Func<T,
		bool>> predicate, Func<T, int> entityIdSelector) where T : class
	{
		int id = 0;
		if (!string.IsNullOrEmpty(key))
			id = _hashids.Decode(key)[0];

		return _uniquenessValidator.IsUnique(repository, predicate, id, entityIdSelector);
	}

	public IActionResult UniqueNationalId(NurseFormViewModel model)
	{
		return CheckUnique(model.Key!, _unitOfWork.Nurses, x => x.NationalId == model.NationalId, d => d.Id);
	}

	public IActionResult UniqueEmail(NurseFormViewModel model)
	{
		return CheckUnique(model.Key!, _unitOfWork.Nurses, x => x.Email == model.Email, d => d.Id);
	}

	public IActionResult UniqueMobileNumber(NurseFormViewModel model)
	{
		return CheckUnique(model.Key!, _unitOfWork.Nurses, x => x.MobileNumber == model.MobileNumber, d => d.Id);
	}
}