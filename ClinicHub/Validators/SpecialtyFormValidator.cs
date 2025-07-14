using ClinicHub.Consts;

namespace ClinicHub.Validators;

public class SpecialtyFormValidator : AbstractValidator<SpecialtyFormViewModel>
{
	public SpecialtyFormValidator()
	{
		RuleFor(x => x.Name).MaximumLength(100).WithMessage(Errors.MaxLength);
		RuleFor(x => x.Description).MaximumLength(500).WithMessage(Errors.MaxLength);
	}
}