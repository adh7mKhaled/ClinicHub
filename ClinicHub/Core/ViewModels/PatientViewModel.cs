namespace ClinicHub.Core.ViewModels;

public class PatientViewModel
{
	public int Id { get; set; }
	public string FullName { get; set; } = null!;
	public DateTime DateOfBirth { get; set; }
	public Gender Gender { get; set; }
	public RegistrationStatus RegistrationStatus { get; set; }
	public MaritalStatus MaritalStatus { get; set; }
	public string PhoneNumber { get; set; } = null!;
	public string Address { get; set; } = null!;
	public bool IsDeleted { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime? LastUpdatedOn { get; set; }
}