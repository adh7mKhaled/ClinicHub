namespace ClinicHub.Controllers;

public class ReportsController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Appointments(int? pageNumber)
    {
        var doctors = _unitOfWork.Doctors.GetQueryable();
        var patients = _unitOfWork.Patients.GetQueryable();

        var appointments = _unitOfWork.Appointments
                            .GetQueryable()
                            .Include(x => x.Doctor)
                            .Include(x => x.Patient);

        AppointmentReportViewModel viewModel = new()
        {
            Doctors = _mapper.Map<IEnumerable<SelectListItem>>(doctors),
            Patients = _mapper.Map<IEnumerable<SelectListItem>>(patients)
        };

        if (pageNumber is not null)
            viewModel.Appointments = PaginatedList<Appointment>.Create(appointments, pageNumber ?? 0, 25);

        return View(viewModel);
    }
}