namespace ClinicHub.Controllers;

public class ReportsController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Appointments(int? pageNumber, IList<int> selectedDoctorsIDs, IList<int> selectedPatientsIDs)
    {
        var doctors = _unitOfWork.Doctors.GetQueryable();
        var patients = _unitOfWork.Patients.GetQueryable();

        IQueryable<Appointment> appointments = _unitOfWork.Appointments
                            .GetQueryable()
                            .Include(x => x.Doctor)
                            .Include(x => x.Patient);

        if (selectedDoctorsIDs.Any())
            appointments = appointments.Where(a => selectedDoctorsIDs.Contains(a.DoctorId));

        if (selectedPatientsIDs.Any())
            appointments = appointments.Where(a => selectedPatientsIDs.Contains(a.PatientId));

        AppointmentReportViewModel viewModel = new()
        {
            Doctors = _mapper.Map<IEnumerable<SelectListItem>>(doctors),
            Patients = _mapper.Map<IEnumerable<SelectListItem>>(patients)
        };

        if (pageNumber is not null)
            viewModel.Appointments = PaginatedList<Appointment>.Create(appointments, pageNumber ?? 0, (int)ReportsConfigurations.PageSize);

        return View(viewModel);
    }
}