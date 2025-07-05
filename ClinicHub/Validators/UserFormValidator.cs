using ClinicHub.Consts;

namespace ClinicHub.Validators;

public class UserFormValidator : AbstractValidator<UserFormViewModel>
{
	public UserFormValidator()
	{
		RuleFor(x => x.FullName)
			.MaximumLength(100).WithMessage(Errors.MaxLength)
			.Matches(RegexPatterns.CharactersOnly_English).WithMessage(Errors.OnlyEnglishLetters);

		RuleFor(x => x.UserName)
			.MaximumLength(50).WithMessage(Errors.MaxLength)
			.Matches(RegexPatterns.Username).WithMessage(Errors.InvalidUsername);

		RuleFor(x => x.Email)
			.MaximumLength(200).WithMessage(Errors.MaxLength)
			.EmailAddress();

		RuleFor(x => x.Password)
			.Length(8, 100).WithMessage(Errors.MaxMinLength)
			.Matches(RegexPatterns.Password).WithMessage(Errors.WeakPassword);

		RuleFor(x => x.ConfirmPassword)
			.Equal(x => x.Password).WithMessage(Errors.ConfirmPasswordNotMatch);
	}
}