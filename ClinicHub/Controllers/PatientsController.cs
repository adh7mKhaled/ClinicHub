namespace ClinicHub.Controllers;

public class PatientsController : Controller
{
	private readonly IPatientServices _patientServices;
	private readonly IMapper _mapper;

	public PatientsController(IPatientServices patientServices, IMapper mapper)
	{
		_patientServices = patientServices;
		_mapper = mapper;
	}

	public IActionResult Index()
	{
		var patients = _patientServices.GetAll();
		var viewModel = _mapper.Map<IEnumerable<PatientViewModel>>(patients);

		return View(viewModel);
	}
}