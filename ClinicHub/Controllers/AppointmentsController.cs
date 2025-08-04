namespace ClinicHub.Controllers;

public class AppointmentsController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;

	public IActionResult Index()
	{
		AppointmentViewModel viewModel = new()
		{
			Specialties = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Specialties.GetAll()),
			Patients = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Patients.GetAll())
		};

		return View(viewModel);
	}

	[AjaxOnly]
	public IActionResult GetDoctors(int specialtyId)
	{
		var doctors = _unitOfWork.Doctors.GetAll()
			.Where(x => x.SpecialtyId == specialtyId)
			.OrderBy(x => x.Name)
			.ToList();

		return Ok(_mapper.Map<IEnumerable<SelectListItem>>(doctors));
	}

	//public IActionResult GetAvailableTimes(DateOnly date, int doctorId)
	//{
	//	var dates = _unitOfWork.DoctorSchedules.GetAll()
	//		.Where(x => x.DoctorId == doctorId && x.DayOfWeek == date.DayOfWeek)
	//		.ToList();

	//	return Ok(_mapper.Map<IEnumerable<SelectListItem>>(dates));
	//}

}