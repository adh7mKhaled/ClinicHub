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
	public IEnumerable<AppointmentViewModel> PastAndUpcomingAppointments { get; set; } = [];
	public IEnumerable<AppointmentViewModel> TodayAppointments { get; set; } = [];
	public int TodayAppointmentsCount { get; set; }
	public ICollection<Nurse> Nurses { get; set; } = [];
	public int NumberOfNurses
	{
		get
		{
			return Nurses.Count;
		}
	}
	public int NumberOfTotalAppointments
	{
		get
		{
			return PastAndUpcomingAppointments.Count() + TodayAppointments.Count();
		}
	}
}