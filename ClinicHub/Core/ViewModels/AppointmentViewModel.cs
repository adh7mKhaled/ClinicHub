namespace ClinicHub.Core.ViewModels;

public class AppointmentViewModel
{
	public int Id { get; set; }
	public string PatientName { get; set; } = null!;
	public string DoctorName { get; set; } = null!;
	public string AppointmentDate { get; set; } = null!;
	public string AppointmentTime { get; set; } = null!;
	public string TimeSlot { get; set; } = null!;
	public AppointmentStatus Status { get; set; }
}