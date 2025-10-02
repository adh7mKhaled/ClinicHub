using AspNetCoreGeneratedDocument;

namespace ClinicHub.Controllers;

[Authorize]
public class DashboardController(IUnitOfWork unitOfWork, IMapper mapper, IHashids hashids): Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;
    private readonly IHashids _hashids = hashids;

    public IActionResult Index()
	{
		var todayAppointments = _unitOfWork.Appointments.GetQueryable()
			.Where(a => a.AppointmentDate == DateTime.Today);

		var rowData = _unitOfWork.Patients.GetAll()
			.Where(p => p.CreatedOn.Year == DateTime.Now.Year &&
					DateTime.Now.Month == p.CreatedOn.Month && 
					!p.IsDeleted)
			.ToList();

		var patientsThisMonth = _mapper.Map<IEnumerable<PatientViewModel>>(rowData);

		foreach (var patient in patientsThisMonth)
			patient.Key = _hashids.Encode(patient.Id);

        DashboardViewModel viewModel = new()
		{
			MonthlyPatientCount = _unitOfWork.Patients.Count(p => p.CreatedOn.Year == DateTime.Now.Year && DateTime.Now.Month == p.CreatedOn.Month),
			NumberOfTodayAppointments = _unitOfWork.Appointments.Count(a => a.AppointmentDate == DateTime.Today),
			MonthlyDoctorCount = _unitOfWork.Doctors.Count(p => p.CreatedOn.Year == DateTime.Now.Year && DateTime.Now.Month == p.CreatedOn.Month),
			TodayAppointments = _mapper.ProjectTo<AppointmentViewModel>(todayAppointments).ToList(),
			PatientsThisMonth = patientsThisMonth,
        };

		return View(viewModel);
	}

	[AjaxOnly]
	public IActionResult GetAppointmentsPerDay(DateTime? startDate, DateTime? endDate)
	{
		startDate ??= DateTime.Today.AddDays(-29);
		endDate ??= DateTime.Today;

		var data = _unitOfWork.Appointments.GetAll()
			.Where(a => a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
			.OrderBy(a => a.AppointmentDate)
			.GroupBy(a => new { a.AppointmentDate })
			.Select(g => new ChartItemViewModel {
				Label = g.Key.AppointmentDate.ToString("d MMM"),
				Value = g.Count().ToString()	
			})
			.ToList();

		return Ok(data);
	}

	[AjaxOnly]
	public IActionResult GetDoctorsPerSpecialty()
	{
		var data = from s in _unitOfWork.Specialties.GetQueryable()
				   join d in _unitOfWork.Doctors.GetQueryable()
				   on s.Id equals d.SpecialtyId
				   into doctorsGroup
				   select new ChartItemViewModel
				   {
					   Label = s.Name,
					   Value = doctorsGroup.Count().ToString()
				   };

        return Ok(data);
	}

	[AjaxOnly]
    public IActionResult GetStats()
    {
        var stats = new
        {
            NumberOfPatients = _unitOfWork.Patients.Count(p => !p.IsDeleted),
            NumberOfAppointments = _unitOfWork.Appointments.Count(),
            NumberOfDoctors = _unitOfWork.Doctors.Count(d => !d.IsDeleted)
        };
        return Json(stats);
    }
}