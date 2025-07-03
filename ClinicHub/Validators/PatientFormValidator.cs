using ClinicHub.Consts;

namespace ClinicHub.Validators;

public class PatientFormValidator : AbstractValidator<PatientFormViewModel>
{
	public PatientFormValidator()
	{
		RuleFor(x => x.Name)
			.MaximumLength(50).WithMessage(Errors.MaxLength)
			.Matches(RegexPatterns.CharactersOnly_English).WithMessage(Errors.OnlyEnglishLetters);

		RuleFor(x => x.City)
			.MaximumLength(50).WithMessage(Errors.MaxLength);

		RuleFor(x => x.MobileNumber)
			.Matches(RegexPatterns.MobileNumber).WithMessage(Errors.InValideMobileNumber);

		RuleFor(x => x.Notes)
			.MaximumLength(1500).WithMessage(Errors.MaxLength);

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();
	}
}