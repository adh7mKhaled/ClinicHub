namespace ClinicHub.Core.ViewModels;

public class PatientSearchResultViewModel
{
	public string? Key { get; set; }
	public string Name { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string MobileNumber { get; set; } = null!;
}