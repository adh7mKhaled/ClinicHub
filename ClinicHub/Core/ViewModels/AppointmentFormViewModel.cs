namespace ClinicHub.Core.ViewModels;

public class AppointmentFormViewModel
{
	public int SpecialtyId { get; set; }

	public int DoctorId { get; set; }

	public int PatientId { get; set; }

	[Display(Name = "Appointment dates")]
	public DateOnly AppointmentDate { get; set; }

	[Display(Name = "Time slots")]
	public TimeSpan AvailableDates { get; set; }

	public IEnumerable<SelectListItem> Specialties { get; set; } = [];
	public IEnumerable<SelectListItem> Patients { get; set; } = [];
	public IEnumerable<SelectListItem> Dates { get; set; } = [];
}