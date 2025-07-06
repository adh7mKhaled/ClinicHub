using ClinicHub.Consts;

namespace ClinicHub.Validators;

public class ResetPasswordFormValidator : AbstractValidator<ResetPasswordFormViewModel>
{
	public ResetPasswordFormValidator()
	{
		RuleFor(x => x.Password)
			.Length(8, 100).WithMessage(Errors.MaxMinLength);

		RuleFor(x => x.Password)
			.Matches(RegexPatterns.Password).WithMessage(Errors.WeakPassword);

		RuleFor(x => x.ConfirmPassword)
			.Equal(x => x.Password).WithMessage(Errors.ConfirmPasswordNotMatch);
	}
}