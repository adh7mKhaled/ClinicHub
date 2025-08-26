using ClinicHub.Data;

namespace ClinicHub.Validators;

public class AppointmentFormValidator : AbstractValidator<AppointmentFormViewModel>
{
	private readonly ApplicationDbContext _context;

	public AppointmentFormValidator(ApplicationDbContext context)
	{
		_context = context;

		RuleFor(x => x)
			.Must(model =>
			{
				bool exists = ExistsAsync(model.DoctorId, model.PatientId, model.AppointmentDate);
				return !exists;
			});
	}

	public bool ExistsAsync(int doctorId, int patientId, DateOnly date)
	{
		var isExists = _context.Appointments
			.Any(x => x.DoctorId == doctorId &&
					x.PatientId == patientId &&
					x.AppointmentDate == date
			);

		return isExists;
	}
}