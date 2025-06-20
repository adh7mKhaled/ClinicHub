namespace ClinicHub.Core.Models;

public class Patient
{
	public int Id { get; set; }
	public string FullName { get; set; } = null!;
	public DateTime DateOfBirth { get; set; }
	public string Gender { get; set; } = null!;
	public bool Status { get; set; }
	public string MaritalStatus { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	public string Address { get; set; } = null!;
	public DateTime CreatedOn { get; set; } = DateTime.Now;
	public DateTime? LastUpdatedOn { get; set; }
}