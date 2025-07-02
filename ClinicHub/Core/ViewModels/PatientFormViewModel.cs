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

	[Display(Name = "Phone Number")]
	public string MobileNumber { get; set; } = null!;
	public string? Email { get; set; }
	public string City { get; set; } = null!;

	[Display(Name = "Has WhatsApp")]
	public bool HasWhatsApp { get; set; }
}