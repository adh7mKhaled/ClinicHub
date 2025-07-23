using ClinicHub.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicHub.Controllers;

public class DoctorsController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<DoctorFormViewModel> validator, IUniquenessValidator uniquenessValidator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<DoctorFormViewModel> _validator = validator;
	private readonly IUniquenessValidator _uniquenessValidator = uniquenessValidator;

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Search(SearchFormViewModel model)
	{
		if (!ModelState.IsValid)
			return View(nameof(Index), model);

		var doctor = _unitOfWork.Doctors.Find(x => x.Email == model.Value || x.MobileNumber == model.Value || x.NationalId == model.Value);

		return PartialView("_Result", _mapper.Map<DoctorSearchResultViewModel>(doctor));
	}

	public IActionResult Details(int id)
	{
		var doctor = _unitOfWork.Doctors.GetById(id);

		if (doctor is null)
			return NotFound();

		var viewModel = _mapper.Map<DoctorViewModel>(doctor);

		var specialty = _unitOfWork.Specialties.GetById(doctor.SpecialtyId);
		viewModel.Specialty = specialty!.Name;

		return View(viewModel);
	}

	public IActionResult Create()
	{
		DoctorFormViewModel viewModel = new()
		{
			Specialties = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Specialties.GetAll())
		};

		return View("Form", viewModel);
	}

	[HttpPost]
	public IActionResult Create(DoctorFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var doctor = _mapper.Map<Doctor>(model);

		doctor.CreatedById = User.GetUserId();
		doctor.CreatedOn = DateTime.Now;

		_unitOfWork.Doctors.Add(doctor);
		_unitOfWork.Complete();

		return RedirectToAction(nameof(Details), new { id = doctor.Id });
	}

	public IActionResult Edit(int id)
	{
		var doctor = _unitOfWork.Doctors.GetById(id);

		if (doctor is null)
			return NotFound();

		var viewModel = _mapper.Map<DoctorFormViewModel>(doctor);

		viewModel.Specialties = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Specialties.GetAll());

		return View("Form", viewModel);
	}

	[HttpPost]
	public IActionResult Edit(DoctorFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var doctor = _mapper.Map<Doctor>(model);

		doctor.LastUpdatedById = User.GetUserId();
		doctor.LastUpdatedOn = DateTime.Now;

		_unitOfWork.Doctors.Update(doctor);
		_unitOfWork.Complete();

		return RedirectToAction(nameof(Details), new { id = doctor.Id });
	}

	public IActionResult UniqueNationalId(DoctorFormViewModel model)
	{
		return _uniquenessValidator.IsUnique(_unitOfWork.Doctors, d => d.NationalId == model.NationalId, model.Id, d => d.Id);
	}

	public IActionResult UniqueEmail(DoctorFormViewModel model)
	{
		return _uniquenessValidator.IsUnique(_unitOfWork.Doctors, x => x.Email == model.Email, model.Id, d => d.Id);
	}

	public IActionResult UniqueMobileNumber(DoctorFormViewModel model)
	{
		return _uniquenessValidator.IsUnique(_unitOfWork.Doctors, x => x.MobileNumber == model.MobileNumber, model.Id, d => d.Id);
	}
}