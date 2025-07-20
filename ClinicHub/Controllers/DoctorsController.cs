using ClinicHub.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicHub.Controllers;

public class DoctorsController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<DoctorFormViewModel> validator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<DoctorFormViewModel> _validator = validator;

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
		var specialties = _unitOfWork.Specialties.GetAll();
		DoctorFormViewModel viewModel = new()
		{
			Specialties = specialties.Select(s => new SelectListItem
			{
				Text = s.Name,
				Value = s.Id.ToString()
			})
		};

		return View(viewModel);
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

		return Ok();
	}
}