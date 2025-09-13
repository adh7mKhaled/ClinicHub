namespace ClinicHub.Core.ViewModels;

public class DashboardViewModel
{
	public int NumberOfPatients { get; set; }
	public int NumberOfMalePatients { get; set; }
	public int NumberOfFemalePatients { get; set; }
	public int NumberOfAppointments { get; set; }
	public int NumberOfTodayAppointments { get; set; }
	public int NumberOfDoctors { get; set; }
	public int NumberOfMaleDoctors { get; set; }
	public int NumberOfFemaleDoctors { get; set; }

	public IEnumerable<AppointmentViewModel> TodayAppointments { get; set; } = [];
}