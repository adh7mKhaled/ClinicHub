﻿namespace ClinicHub.Core.ViewModels;

public class UserViewModel
{
	public string Id { get; set; } = null!;
	public string FullName { get; set; } = null!;
	public string UserName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public bool IsDeleted { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime? LastUpdatedOn { get; set; }
}