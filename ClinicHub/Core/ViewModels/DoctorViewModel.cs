namespace ClinicHub.Core.ViewModels;

public class DoctorViewModel
{
	public string? Key { get; set; }
	public string Name { get; set; } = null!;
	public string NationalId { get; set; } = null!;
	public int Age { get; set; }
	public Gender Gender { get; set; }
	public string Email { get; set; } = null!;
	public string MobileNumber { get; set; } = null!;
	public string Address { get; set; } = null!;
	public decimal Salary { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime CreatedOn { get; set; }
	public string Specialty { get; set; } = null!;

	public IEnumerable<DoctorSchedule> DoctorSchedules { get; set; } = [];
	public IEnumerable<AppointmentViewModel> PastAppointments { get; set; } = [];
	public IEnumerable<AppointmentViewModel> UpcomingAppointments { get; set; } = [];
	public IEnumerable<AppointmentViewModel> TodayAppointments { get; set; } = [];

	public int NoOfTodayAppointments => TodayAppointments.Count();
	public int NoOfUpcomingAppointments => UpcomingAppointments.Count();
	public int NoOfTotalAppointments => 
		PastAppointments.Count() + TodayAppointments.Count() + UpcomingAppointments.Count();
}