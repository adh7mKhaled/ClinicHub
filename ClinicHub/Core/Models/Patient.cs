namespace ClinicHub.Core.Models;

public class Patient : BaseModel
{
	public int Id { get; set; }
	public string FullName { get; set; } = null!;
	public DateTime DateOfBirth { get; set; }
	public Gender Gender { get; set; }
	public bool Status { get; set; }
	public MaritalStatus MaritalStatus { get; set; }
	public string PhoneNumber { get; set; } = null!;
	public string Address { get; set; } = null!;
}