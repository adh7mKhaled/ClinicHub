﻿namespace ClinicHub.Consts
{
	public static class RegexPatterns
	{
		public const string Password = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
		public const string Username = "^[a-zA-Z0-9-._@+]*$";
		public const string CharactersOnly_English = "^[a-zA-Z-_ ]*$";
		public const string CharactersOnly_Arabic = "^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF ]*$";
		public const string NumbersAndChrOnly_ArEng = "^(?=.*[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z])[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9 _-]+$";
		public const string DenySpecialCharacters = "^[^<>!#%$]*$";
		public const string MobileNumber = "^01[0,1,2,5]{1}[0-9]{8}$";
		public const string NationalID = "^[2,3]{1}[0-9]{13}$";
		public const string NumbersOnly = "^(?:-(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))|(?:0|(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))))(?:.\\d+|)$";
	}
}