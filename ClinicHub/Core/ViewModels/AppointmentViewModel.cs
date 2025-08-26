namespace ClinicHub.Core.ViewModels;

public class AppointmentViewModel
{
	public string PatientName { get; set; } = null!;
	public string DoctorName { get; set; } = null!;
	public DateOnly AppointmentDate { get; set; }
	public DateTime TimeSlot { get; set; }
}