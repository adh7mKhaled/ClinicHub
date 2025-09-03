namespace ClinicHub.Core.Models;

public class Patient : BaseModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public DateTime DateOfBirth { get; set; }
	public Gender Gender { get; set; }
	public MaritalStatus MaritalStatus { get; set; }
	public string? Notes { get; set; }
	public string MobileNumber { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string City { get; set; } = null!;
	public bool HasWhatsApp { get; set; }
}