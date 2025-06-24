using ClinicHub.Consts;

namespace ClinicHub.Core.ViewModels;

public class PatientFormViewModel
{
	public int Id { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;

	[AssertThat("DateOfBirth < Today()", ErrorMessage = Errors.NotAllowDates)]
	public DateTime DateOfBirth { get; set; }

	public Gender Gender { get; set; }
	public MaritalStatus MaritalStatus { get; set; }
	public string? Notes { get; set; }
	public string PhoneNumber { get; set; } = null!;
	public string Address { get; set; } = null!;

	[Display(Name = "Has WhatsApp")]
	public bool HasWhatsApp { get; set; }
}