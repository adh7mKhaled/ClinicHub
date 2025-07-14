using ClinicHub.Consts;

namespace ClinicHub.Core.ViewModels;

public class SpecialtyFormViewModel
{
	public int Id { get; set; }

	[Remote("AllowUniqueName", null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
}