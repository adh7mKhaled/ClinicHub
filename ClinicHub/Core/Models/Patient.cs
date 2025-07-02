namespace ClinicHub.Core.Models;

public class Patient : BaseModel
{
	public int Id { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public DateTime DateOfBirth { get; set; }
	public Gender Gender { get; set; }
	public RegistrationStatus RegistrationStatus { get; set; } = RegistrationStatus.Pending;
	public MaritalStatus MaritalStatus { get; set; }
	public string MobileNumber { get; set; } = null!;
	public string? Email { get; set; }
	public string City { get; set; } = null!;
	public bool HasWhatsApp { get; set; }
}