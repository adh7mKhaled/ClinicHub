namespace ClinicHub.Core.ViewModels;

public class AppointmentFormViewModel
{
	[Display(Name = "Specialty")]
	public int SpecialtyId { get; set; }

	[Display(Name = "Doctor")]
	public int DoctorId { get; set; }

	[Display(Name = "Patient")]
	public int PatientId { get; set; }

	[Display(Name = "Appointment dates")]
	[AssertThat("AppointmentDate >= Today()", ErrorMessage = Errors.NotAllowedPastDates)]
	public DateTime AppointmentDate { get; set; }

	[Display(Name = "Available time slots")]
	public TimeSpan AvailableDates { get; set; }

	public IEnumerable<SelectListItem> Specialties { get; set; } = [];
	public IEnumerable<SelectListItem> Patients { get; set; } = [];
	public IEnumerable<SelectListItem> Dates { get; set; } = [];
}