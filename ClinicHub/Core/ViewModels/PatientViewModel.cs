namespace ClinicHub.Core.ViewModels;

public class PatientViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public DateTime DateOfBirth { get; set; }
	public Gender Gender { get; set; }
	public MemberStatus MemberStatus { get; set; }
	public MaritalStatus MaritalStatus { get; set; }
	public string? Notes { get; set; }
	public string MobileNumber { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string City { get; set; } = null!;
	public DateTime CreatedOn { get; set; }
	public DateTime? LastUpdatedOn { get; set; }
}