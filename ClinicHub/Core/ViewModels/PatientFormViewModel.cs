using ClinicHub.Consts;

namespace ClinicHub.Core.ViewModels;

public class PatientFormViewModel
{
	public int Id { get; set; }

	[Display(Name = "First Name")]
	public string Name { get; set; } = null!;

	[AssertThat("DateOfBirth < Today()", ErrorMessage = Errors.NotAllowFutureDate)]
	[Display(Name = "Date Of Birth")]
	public DateTime DateOfBirth { get; set; }

	public Gender Gender { get; set; }

	[Display(Name = "Marital Status")]
	public MaritalStatus MaritalStatus { get; set; }

	public string? Notes { get; set; }

	[Display(Name = "Phone Number"),
		Remote("AllowUniqueMobileNumber", null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
	public string MobileNumber { get; set; } = null!;

	[Remote("AllowUniqueEmail", null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
	public string Email { get; set; } = null!;

	public string City { get; set; } = null!;

	[Display(Name = "Has WhatsApp")]
	public bool HasWhatsApp { get; set; }
}