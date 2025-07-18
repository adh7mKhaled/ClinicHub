using ClinicHub.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicHub.Controllers;

public class DoctorsController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<DoctorFormViewModel> validator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<DoctorFormViewModel> _validator = validator;

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

		return Ok();
	}
}