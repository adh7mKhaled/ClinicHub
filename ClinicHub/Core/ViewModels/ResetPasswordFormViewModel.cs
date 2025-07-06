namespace ClinicHub.Core.ViewModels;

public class ResetPasswordFormViewModel
{
	public string Id { get; set; } = null!;

	[DataType(DataType.Password)]
	public string Password { get; set; } = null!;

	[DataType(DataType.Password), Display(Name = "Confirm Password")]
	public string ConfirmPassword { get; set; } = null!;

}