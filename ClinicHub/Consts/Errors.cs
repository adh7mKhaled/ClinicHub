namespace ClinicHub.Consts
{
	public static class Errors
	{
		public const string RequiredField = "Required field";
		public const string MaxLength = "Length cannot be more than {MaxLength} characters";
		public const string MaxMinLength = "The {PropertyName} must be between {From} and {To}.";
		public const string Duplicated = "Another record with the same {0} is already exist";
		public const string DuplicatedBook = "Book with the same title is already exists with the same author";
		public const string NotAllowedExtension = "Only .png, .jpg, .jpeg files are allowed!";
		public const string MaxSize = "File cannot be more that 2 MB!";
		public const string NotAllowDates = "Date cannot be today!";
		public const string InvalidRange = "{PropertyName} shouid be between {From} and {To}!";
		public const string ConfirmPasswordNotMatch = "The password and confirmation password do not match.";
		public const string WeakPassword = "passwords must contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least 8 characters long.";
		public const string InvalidUsername = "Username can only contain letters or digits.";
		public const string OnlyEnglishLetters = "Only English letters are allowed.";
		public const string OnlyArablicLetters = "Only Arabic letters are allowed.";
		public const string OnlyNumbersAndLetters = "Only Arabic/English letters or digits are allowed.";
		public const string DenySpecialCharacters = "Special Characters are not allowed.";
		public const string InValideMobileNumber = "Invalide mobile number";
		public const string InValideNationalID = "Invalide NationalID";
		public const string InValideSerialNumber = "Invalide serial number.";
		public const string NotAvailableRental = "This book/copy is not available for rental.";
		public const string EmptyImage = "Please select an image.";
		public const string BlackListedSubscriber = "This subscriber is black listed";
		public const string InactiveSubscriber = "This subscriber is Inactive";
		public const string MaxCopiesReached = "This subscriber has reached to max number for rentals";
		public const string CopyIsInRental = "This Copy is already rentaled";
		public const string RentalNotAllowedForBlackListed = "Rental cannot be extended for blacklisted subscribers.";
		public const string RentalNotAllowedForInactive = "Rental cannot be extended for this subscriber before renewal.";
		public const string ExtendNotAllowed = "Rental cannot be extended.";
		public const string PenaltyShouldBePaid = "Penalty should be paid.";
		public const string InvalidStartDate = "Invalid start date.";
		public const string InvalidEndDate = "Invalid end date.";
	}
}