namespace ClinicHub.Core.ViewModels;

public class AppointmentFormViewModel
{
	public int SpecialtyId { get; set; }

	public int DoctorId { get; set; }

	public int PatientId { get; set; }

	[Display(Name = "Appointment dates")]
	[AssertThat("AppointmentDate >= Today()", ErrorMessage = Errors.NotAllowedPastDates)]
	public DateTime AppointmentDate { get; set; } = DateTime.Today;

	[Display(Name = "Time slots")]
	public TimeSpan AvailableDates { get; set; }

	public IEnumerable<SelectListItem> Specialties { get; set; } = [];
	public IEnumerable<SelectListItem> Patients { get; set; } = [];
	public IEnumerable<SelectListItem> Dates { get; set; } = [];
}