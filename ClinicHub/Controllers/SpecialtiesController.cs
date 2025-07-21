using ClinicHub.Data.UnitOfWork;

namespace ClinicHub.Controllers;

public class SpecialtiesController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<SpecialtyFormViewModel> validator, IUniquenessValidator uniquenessValidator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<SpecialtyFormViewModel> _validator = validator;
	private readonly IUniquenessValidator _uniquenessValidator = uniquenessValidator;

	public IActionResult Index()
	{
		var specialties = _unitOfWork.Specialties.GetAll();
		return View(_mapper.Map<IEnumerable<SpecialtyViewModel>>(specialties));
	}

	[AjaxOnly]
	public IActionResult Create()
	{
		return PartialView("_Form");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(SpecialtyFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var specialty = _mapper.Map<Specialty>(model);

		specialty.CreatedById = User.GetUserId();

		_unitOfWork.Specialties.Add(specialty);
		_unitOfWork.Complete();

		return PartialView("_SpecialtyRow", _mapper.Map<SpecialtyViewModel>(specialty));
	}

	[AjaxOnly]
	public IActionResult Edit(int id)
	{
		var specialty = _unitOfWork.Specialties.GetById(id);

		if (specialty is null)
			return NotFound();

		return PartialView("_Form", _mapper.Map<SpecialtyFormViewModel>(specialty));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Edit(SpecialtyFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var specialty = _mapper.Map<Specialty>(model);
		specialty.LastUpdatedById = User.GetUserId();
		specialty.LastUpdatedOn = DateTime.Now;

		_unitOfWork.Specialties.Update(specialty);
		_unitOfWork.Complete();

		return PartialView("_SpecialtyRow", _mapper.Map<SpecialtyViewModel>(specialty));
	}

	public IActionResult ToggleStatus(int id)
	{
		var specialty = _unitOfWork.Specialties.GetById(id);

		if (specialty is null)
			return NotFound();

		specialty.IsDeleted = !specialty.IsDeleted;
		specialty.LastUpdatedById = User.GetUserId();
		specialty.LastUpdatedOn = DateTime.Now;

		_unitOfWork.Complete();

		return Ok(specialty.LastUpdatedOn.ToString());
	}

	public IActionResult AllowUniqueName(SpecialtyFormViewModel model)
	{
		return _uniquenessValidator.IsUnique(_unitOfWork.Specialties, s => s.Name == model.Name, model.Id, s => s.Id);
	}
}