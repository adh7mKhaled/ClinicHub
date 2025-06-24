using ClinicHub.Consts;

namespace ClinicHub.Core.ViewModels;

public class PatientFormViewModel
{
	public int Id { get; set; }

	[Display(Name = "First Name")]
	public string FirstName { get; set; } = null!;

	[Display(Name = "Last Name")]
	public string LastName { get; set; } = null!;

	[AssertThat("DateOfBirth < Today()", ErrorMessage = Errors.NotAllowDates)]
	[Display(Name = "Date Of Birth")]
	public DateTime DateOfBirth { get; set; }

	public Gender Gender { get; set; }

	[Display(Name = "Marital Status")]
	public MaritalStatus MaritalStatus { get; set; }
	public string? Notes { get; set; }

	[Display(Name = "Phone Number")]
	public string PhoneNumber { get; set; } = null!;
	public string Address { get; set; } = null!;

	[Display(Name = "Has WhatsApp")]
	public bool HasWhatsApp { get; set; }
}