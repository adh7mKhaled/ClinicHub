using ClinicHub.Data.UnitOfWork;
using ClinicHub.Extensions;

namespace ClinicHub.Controllers;

public class SpecialtiesController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<SpecialtyFormViewModel> validator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<SpecialtyFormViewModel> _validator = validator;

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
		var specialty = _unitOfWork.Specialties.Find(x => x.Name == model.Name);

		var isAllowed = specialty is null || specialty.Id.Equals(model.Id);

		return Json(isAllowed);
	}
}