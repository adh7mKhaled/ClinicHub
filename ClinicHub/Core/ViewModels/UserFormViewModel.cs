using ClinicHub.Consts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicHub.Core.ViewModels;

public class UserFormViewModel
{
	public string? Id { get; set; }

	[Display(Name = "Full Name")]
	public string FullName { get; set; } = null!;

	[Display(Name = "User Name")]
	public string UserName { get; set; } = null!;

	public string Email { get; set; } = null!;

	[DataType(DataType.Password),
		RequiredIf("Id == null", ErrorMessage = Errors.RequiredField)]
	public string? Password { get; set; } = null!;

	[DataType(DataType.Password), Display(Name = "Confirm password"),
		RequiredIf("Id == null", ErrorMessage = Errors.RequiredField)]
	public string? ConfirmPassword { get; set; } = null!;

	[Display(Name = "Roles")]
	public IList<string> SelectedRoles { get; set; } = new List<string>();

	public IEnumerable<SelectListItem>? Roles { get; set; }
}
