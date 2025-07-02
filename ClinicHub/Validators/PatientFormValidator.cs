using ClinicHub.Consts;

namespace ClinicHub.Validators;

public class PatientFormValidator : AbstractValidator<PatientFormViewModel>
{
	public PatientFormValidator()
	{
		RuleFor(x => x.FirstName)
			.MaximumLength(50).WithMessage(Errors.MaxLength)
			.Matches(RegexPatterns.CharactersOnly_English).WithMessage(Errors.OnlyEnglishLetters);

		RuleFor(x => x.LastName)
			.MaximumLength(50).WithMessage(Errors.MaxLength)
			.Matches(RegexPatterns.CharactersOnly_English).WithMessage(Errors.OnlyEnglishLetters);

		RuleFor(x => x.City)
			.MaximumLength(50).WithMessage(Errors.MaxLength);

		RuleFor(x => x.MobileNumber)
			.Matches(RegexPatterns.MobileNumber).WithMessage(Errors.InValideMobileNumber);

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();
	}
}