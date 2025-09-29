namespace ClinicHub.Core.ViewModels;

public class DashboardViewModel
{
	public int NumberOfPatients { get; set; }
	public int MonthlyPatientCount { get; set; }
	public int NumberOfAppointments { get; set; }
	public int NumberOfTodayAppointments { get; set; }
	public int NumberOfDoctors { get; set; }
	public int MonthlyDoctorCount { get; set; }

	public IEnumerable<AppointmentViewModel> TodayAppointments { get; set; } = [];
}