namespace ClinicHub.Core.ViewModels;

public class DashboardViewModel
{
	public int MonthlyPatientCount { get; set; }
	public int NumberOfTodayAppointments { get; set; }
	public int MonthlyDoctorCount { get; set; }

	public IEnumerable<AppointmentViewModel> TodayAppointments { get; set; } = [];
	public IEnumerable<PatientViewModel> PatientsThisMonth { get; set; } = [];
}