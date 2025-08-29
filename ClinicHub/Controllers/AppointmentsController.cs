using ClinicHub.Core.Models;

namespace ClinicHub.Controllers;

public class AppointmentsController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AppointmentFormViewModel> validator) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<AppointmentFormViewModel> _validator = validator;

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost, IgnoreAntiforgeryToken]
	public IActionResult GetAppointments(bool todayOnly)
	{
		var skip = int.Parse(Request.Form["start"]!);
		var pageSize = int.Parse(Request.Form["length"]!);

		var searchValue = Request.Form["search[value]"];

		IQueryable<Appointment> query = _unitOfWork.Appointments.GetQueryable()
			.Include(x => x.Doctor)
			.Include(x => x.Patient);

		if (todayOnly)
			query = query.Where(x => x.AppointmentDate == DateTime.Today);

		if (!string.IsNullOrEmpty(searchValue))
			query = query.Where(x => x.Patient!.Name.Contains(searchValue!) || x.Doctor!.Name.Contains(searchValue!));

		var data = query.Skip(skip).Take(pageSize).ToList();

		var mappedData = _mapper.Map<IEnumerable<AppointmentViewModel>>(data);

		var recordsTotal = query.Count();

		var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = mappedData };

		return Ok(jsonData);
	}

	public IActionResult Create()
	{
		AppointmentFormViewModel viewModel = new()
		{
			Specialties = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Specialties.GetAll()),
			Patients = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Patients.GetAll())
		};

		return View("Form", viewModel);
	}

	[HttpPost]
	public IActionResult Create(AppointmentFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var appointment = new Appointment
		{
			PatientId = model.PatientId,
			DoctorId = model.DoctorId,
			AppointmentDate = model.AppointmentDate,
			AppointmentTime = model.AvailableDates
		};

		_unitOfWork.Appointments.Add(appointment);
		_unitOfWork.Complete();

		return Ok();
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

	[AjaxOnly]
	public IActionResult GetAvailableTimes(DateTime date, int doctorId)
	{
		var schedule = _unitOfWork.DoctorSchedules.GetAll()
			.Where(x => !x.IsDeleted)
			.FirstOrDefault(x => x.DoctorId == doctorId && x.DayOfWeek == date.DayOfWeek);

		if (schedule == null)
			return Ok(new List<SelectListItem>());

		var slots = GenerateTimeSlots(schedule.StartTime, schedule.EndTime, schedule.BookingInterval);

		var bookedTimes = _unitOfWork.Appointments.GetAll()
			.Where(a => a.DoctorId == doctorId && a.AppointmentDate == date)
			.Select(a => a.AppointmentTime)
			.ToList();

		var availableSlots = slots.Where(s => !bookedTimes.Contains(s)).ToList();

		var list = availableSlots.Select(t => new SelectListItem
		{
			Value = t.ToString(@"hh\:mm"),
			Text = DateTime.Today.Add(t).ToString("hh:mm tt")
		});

		return Ok(list);
	}

	private List<TimeSpan> GenerateTimeSlots(TimeSpan startTime, TimeSpan endTime, int BookingInterval)
	{
		var slots = new List<TimeSpan>();
		var current = startTime;
		while (current.Add(TimeSpan.FromMinutes(BookingInterval)) <= endTime)
		{
			slots.Add(current);
			current = current.Add(TimeSpan.FromMinutes(BookingInterval));
		}
		return slots;
	}

}