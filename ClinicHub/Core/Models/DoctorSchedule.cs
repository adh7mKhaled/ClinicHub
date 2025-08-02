namespace ClinicHub.Core.Models;

public class DoctorSchedule
{
	public int Id { get; set; }
	public int DoctorId { get; set; }
	public DayOfWeek DayOfWeek { get; set; }
	public TimeSpan StartTime { get; set; }
	public TimeSpan EndTime { get; set; }
	public bool IsDeleted { get; set; }

	public Doctor Doctor { get; set; } = null!;
}