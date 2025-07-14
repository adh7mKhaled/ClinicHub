using ClinicHub.Extensions;
using ClinicHub.Services.Specialties;

namespace ClinicHub.Controllers;

public class SpecialtiesController(ISpecialtyServices specialtyServices, IMapper mapper,
	IValidator<SpecialtyFormViewModel> validator) : Controller
{
	private readonly ISpecialtyServices _specialtyServices = specialtyServices;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<SpecialtyFormViewModel> _validator = validator;

	public IActionResult Index()
	{
		var specialties = _specialtyServices.GetAll();
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

		_specialtyServices.Add(specialty);

		return PartialView("_SpecialtyRow", _mapper.Map<SpecialtyViewModel>(specialty));
	}

	[AjaxOnly]
	public IActionResult Edit(int id)
	{
		var specialty = _specialtyServices.GetById(id);

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

		_specialtyServices.Edit(specialty);

		return PartialView("_SpecialtyRow", _mapper.Map<SpecialtyViewModel>(specialty));
	}

	public IActionResult ToggleStatus(int id)
	{
		var specialty = _specialtyServices.GetById(id);

		if (specialty is null)
			return NotFound();

		specialty.IsDeleted = !specialty.IsDeleted;
		specialty.LastUpdatedById = User.GetUserId();
		specialty.LastUpdatedOn = DateTime.Now;

		_specialtyServices.Save();

		return PartialView("_SpecialtyRow", _mapper.Map<SpecialtyViewModel>(specialty));
	}

	public IActionResult AllowUniqueName(SpecialtyFormViewModel model)
	{
		var specialty = _specialtyServices.Find(x => x.Name == model.Name);

		var isAllowed = specialty is null || specialty.Id.Equals(model.Id);

		return Json(isAllowed);
	}
}