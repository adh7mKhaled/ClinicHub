namespace ClinicHub.Controllers;

[Authorize]
public class DashboardController(IUnitOfWork unitOfWork, IMapper mapper): Controller
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;

	public IActionResult Index()
	{

		var todayAppointments = _unitOfWork.Appointments.GetQueryable()
			.Where(a => a.AppointmentDate == DateTime.Today);

        DashboardViewModel viewModel = new()
		{
			NumberOfPatients = _unitOfWork.Patients.Count(p => !p.IsDeleted),
			MonthlyPatientCount = _unitOfWork.Patients.Count(p => p.CreatedOn.Year == DateTime.Now.Year && DateTime.Now.Month == p.CreatedOn.Month),
			NumberOfAppointments = _unitOfWork.Appointments.Count(),
			NumberOfTodayAppointments = _unitOfWork.Appointments.Count(a => a.AppointmentDate == DateTime.Today),
			NumberOfDoctors = _unitOfWork.Doctors.Count(d => !d.IsDeleted),
			MonthlyDoctorCount = _unitOfWork.Doctors.Count(p => p.CreatedOn.Year == DateTime.Now.Year && DateTime.Now.Month == p.CreatedOn.Month),
			TodayAppointments = _mapper.ProjectTo<AppointmentViewModel>(todayAppointments).ToList()
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
			.GroupBy(a => new { a.AppointmentDate })
			.Select(g => new ChartItemViewModel {
				Label = g.Key.AppointmentDate.ToString("d MMM"),
				Value = g.Count().ToString()	
			})
			.OrderBy(x => x.Label)
			.ToList();

		return Ok(data);
	}
}