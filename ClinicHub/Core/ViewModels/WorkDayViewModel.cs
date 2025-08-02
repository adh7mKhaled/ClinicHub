namespace ClinicHub.Core.ViewModels;

public class WorkDayViewModel
{
	public DayOfWeek Day { get; set; }

	public bool IsSelected { get; set; }

	[RequiredIf("IsSelected == true")]
	public TimeSpan? StartTime { get; set; }

	[RequiredIf("IsSelected == true"),
		AssertThat("EndTime > StartTime")]
	public TimeSpan? EndTime { get; set; }

	public bool IsDeleted { get; set; }
}