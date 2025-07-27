using ClinicHub.Consts;

namespace ClinicHub.Validators;

public class NurseFormValidator : AbstractValidator<NurseFormViewModel>
{
	public NurseFormValidator()
	{
		RuleFor(x => x.Name)
			.MaximumLength(50).WithMessage(Errors.MaxLength)
			.Matches(RegexPatterns.CharactersOnly_English).WithMessage(Errors.OnlyEnglishLetters);

		RuleFor(x => x.Address)
			.MaximumLength(100).WithMessage(Errors.MaxLength);

		RuleFor(x => x.MobileNumber)
			.Matches(RegexPatterns.MobileNumber).WithMessage(Errors.InValideMobileNumber);

		RuleFor(x => x.NationalId)
			.Matches(RegexPatterns.NationalID).WithMessage(Errors.InValideNationalID);

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();

		RuleFor(x => x.Age)
			.InclusiveBetween(25, 60).WithMessage(Errors.MaxMinLength);

		RuleFor(x => x.Salary)
			.NotNull()
			.GreaterThan(0);
	}
}