namespace ClinicHub.Core.ViewModels;

public class AppointmentViewModel
{
	public int SpecialtyId { get; set; }
	public int DoctorId { get; set; }
	public DateOnly AppointmentDate { get; set; }
	public DateOnly AvailableDates { get; set; }
	public IEnumerable<SelectListItem> Specialties { get; set; } = [];
	public IEnumerable<SelectListItem> Patients { get; set; } = [];
	public IEnumerable<SelectListItem> Dates { get; set; } = [];
}