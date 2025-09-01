using ClinicHub.Data.Repositories;

namespace ClinicHub.Controllers;

public class DoctorsController(IUnitOfWork unitOfWork, IMapper mapper,
	IValidator<DoctorFormViewModel> validator, IUniquenessValidator uniquenessValidator,
	IHashids hashids) : Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
	private readonly IValidator<DoctorFormViewModel> _validator = validator;
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

		var doctor = _unitOfWork.Doctors.Find(x => x.Email == model.Value || x.MobileNumber == model.Value || x.NationalId == model.Value);

		var viewModel = _mapper.Map<DoctorSearchResultViewModel>(doctor);

		if (doctor is not null)
			viewModel.Key = _hashids.Encode(doctor.Id);

		return PartialView("_Result", viewModel);
	}

	public IActionResult Details(string key)
	{
		var doctorId = _hashids.Decode(key)[0];
		var doctor = _unitOfWork.Doctors.GetById(doctorId);

		if (doctor is null)
			return NotFound();

		var viewModel = _mapper.Map<DoctorViewModel>(doctor);

		viewModel.DoctorSchedules = [.. _unitOfWork.DoctorSchedules.GetAll().Where(x => x.DoctorId == doctorId && !x.IsDeleted)];

		var appointments = _unitOfWork.Appointments.GetQueryable()
			.Include(a => a.Patient)
			.Include(a => a.Doctor)
			.Where(x => x.DoctorId == doctorId)
			.OrderByDescending(a => a.AppointmentDate);

		var PastAppointments = appointments
			.Where(a => a.AppointmentDate.Date < DateTime.Today);

		var TodayAppointments = appointments
			.Where(a => a.AppointmentDate.Date == DateTime.Today);

		var upcomingAppointments = appointments
			.Where(a => a.AppointmentDate.Date > DateTime.Today);

		viewModel.PastAppointments = _mapper.Map<IEnumerable<AppointmentViewModel>>(PastAppointments);
		viewModel.TodayAppointments = _mapper.Map<IEnumerable<AppointmentViewModel>>(TodayAppointments);
		viewModel.UpcomingAppointments = _mapper.Map<IEnumerable<AppointmentViewModel>>(upcomingAppointments);

		var specialty = _unitOfWork.Specialties.GetById(doctor.SpecialtyId);
		viewModel.Specialty = specialty!.Name;
		viewModel.Key = key;

		return View(viewModel);
	}

	public IActionResult Create()
	{
		DoctorFormViewModel viewModel = new()
		{
			Specialties = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Specialties.GetAll()),

			WorkDays = Enum.GetValues<DayOfWeek>()
			.Select(d => new WorkDayViewModel { Day = d })
			.ToList()
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

		var schedules = model.WorkDays
		.Where(x => x.IsSelected)
		.Select(d => new DoctorSchedule
		{
			DoctorId = doctor.Id,
			DayOfWeek = d.Day,
			StartTime = d.StartTime!.Value,
			EndTime = d.EndTime!.Value
		});

		_unitOfWork.DoctorSchedules.AddRange(schedules);
		_unitOfWork.Complete();

		return RedirectToAction(nameof(Details), new { key = _hashids.Encode(doctor.Id) });
	}

	public IActionResult Edit(string key)
	{
		var doctorId = _hashids.Decode(key)[0];
		var doctor = _unitOfWork.Doctors.GetById(doctorId);

		if (doctor is null)
			return NotFound();

		var viewModel = _mapper.Map<DoctorFormViewModel>(doctor);
		viewModel.Specialties = _mapper.Map<IEnumerable<SelectListItem>>(_unitOfWork.Specialties.GetAll());
		viewModel.Key = key;

		var workDays = Enum.GetValues<DayOfWeek>()
			.Select(d => new WorkDayViewModel { Day = d })
			.ToList();

		var existingSchedules = _unitOfWork.DoctorSchedules.GetAll()
			.Where(x => x.DoctorId == doctor.Id && !x.IsDeleted)
			.ToList();

		foreach (var schedule in workDays)
		{
			var match = existingSchedules.FirstOrDefault(x => x.DayOfWeek == schedule.Day);

			if (match is not null)
			{
				schedule.Day = match.DayOfWeek;
				schedule.StartTime = match.StartTime;
				schedule.EndTime = match.EndTime;
				schedule.IsSelected = true;

				existingSchedules.Remove(match);
			}
		}

		viewModel.WorkDays = workDays;

		return View("Form", viewModel);
	}

	[HttpPost]
	public IActionResult Edit(DoctorFormViewModel model)
	{
		var validationResult = _validator.Validate(model);

		if (!validationResult.IsValid)
			return BadRequest();

		var doctor = _mapper.Map<Doctor>(model);

		doctor.Id = _hashids.Decode(model.Key)[0];
		doctor.LastUpdatedById = User.GetUserId();
		doctor.LastUpdatedOn = DateTime.Now;

		_unitOfWork.Doctors.Update(doctor);
		_unitOfWork.Complete();

		var existingSchedules = _unitOfWork.DoctorSchedules.GetAll()
			.Where(x => x.DoctorId == doctor.Id && !x.IsDeleted)
			.ToList();

		var newSchedules = model.WorkDays
			.Where(x => x.IsSelected)
			.Select(d => new DoctorSchedule
			{
				DoctorId = doctor.Id,
				DayOfWeek = d.Day,
				StartTime = d.StartTime!.Value,
				EndTime = d.EndTime!.Value
			})
			.ToList();

		foreach (var existing in existingSchedules)
		{
			var match = newSchedules.FirstOrDefault(s => s.DayOfWeek == existing.DayOfWeek);
			if (match is not null)
			{
				if (existing.StartTime != match.StartTime || existing.EndTime != match.EndTime)
				{
					existing.StartTime = match.StartTime;
					existing.EndTime = match.EndTime;
					_unitOfWork.DoctorSchedules.Update(existing);
				}

				newSchedules.Remove(match);
			}
			else
			{
				existing.IsDeleted = true;
				_unitOfWork.DoctorSchedules.Update(existing);
			}
		}

		if (newSchedules.Any())
			_unitOfWork.DoctorSchedules.AddRange(newSchedules);

		_unitOfWork.Complete();

		return RedirectToAction(nameof(Details), new { key = model.Key });
	}

	private IActionResult CheckUnique<T>(string key, IBaseRepository<T> repository, Expression<Func<T,
		bool>> predicate, Func<T, int> entityIdSelector) where T : class
	{
		int id = 0;
		if (!string.IsNullOrEmpty(key))
			id = _hashids.Decode(key)[0];

		return _uniquenessValidator.IsUnique(repository, predicate, id, entityIdSelector);
	}

	public IActionResult UniqueNationalId(DoctorFormViewModel model)
	{
		return CheckUnique(model.Key!, _unitOfWork.Doctors, x => x.NationalId == model.NationalId, d => d.Id);
	}

	public IActionResult UniqueEmail(DoctorFormViewModel model)
	{
		return CheckUnique(model.Key!, _unitOfWork.Doctors, x => x.Email == model.Email, d => d.Id);
	}

	public IActionResult UniqueMobileNumber(DoctorFormViewModel model)
	{
		return CheckUnique(model.Key!, _unitOfWork.Doctors, x => x.MobileNumber == model.MobileNumber, d => d.Id);
	}
}