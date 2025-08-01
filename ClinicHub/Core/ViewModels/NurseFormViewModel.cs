using ClinicHub.Consts;

namespace ClinicHub.Core.ViewModels;

public class NurseFormViewModel
{
	public int Id { get; set; }

	public string? Key { get; set; }

	public string Name { get; set; } = null!;

	[Display(Name = "National ID"),
	Remote("UniqueNationalId", controller: null!, AdditionalFields = "Key", ErrorMessage = Errors.Duplicated)]
	public string NationalId { get; set; } = null!;

	public int Age { get; set; }

	public Gender Gender { get; set; }

	[Remote("UniqueEmail", controller: null!, AdditionalFields = "Key", ErrorMessage = Errors.Duplicated)]
	public string Email { get; set; } = null!;

	[Display(Name = "Mobile Number"),
		Remote("UniqueMobileNumber", controller: null!, AdditionalFields = "Key", ErrorMessage = Errors.Duplicated)]
	public string MobileNumber { get; set; } = null!;

	public string Address { get; set; } = null!;

	public decimal? Salary { get; set; }

	public DateOnly HireDate { get; set; }

	[Display(Name = "Doctor")]
	public int DoctorId { get; set; }

	public IEnumerable<SelectListItem> Doctors { get; set; } = [];
}