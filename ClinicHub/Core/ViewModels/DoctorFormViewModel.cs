using ClinicHub.Consts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicHub.Core.ViewModels;

public class DoctorFormViewModel
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	[Remote("UniqueNationalId", controller: null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
	public string NationalId { get; set; } = null!;

	public int Age { get; set; }

	public Gender Gender { get; set; }

	[Remote("UniqueEmail", controller: null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
	public string Email { get; set; } = null!;

	[Remote("UniqueMobileNumber", controller: null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
	public string MobileNumber { get; set; } = null!;

	public string Address { get; set; } = null!;

	public decimal Salary { get; set; }

	public int SpecialtyId { get; set; }

	public IEnumerable<SelectListItem>? Specialties { get; set; } = [];
}