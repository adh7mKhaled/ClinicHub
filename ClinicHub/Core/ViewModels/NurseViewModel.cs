﻿namespace ClinicHub.Core.ViewModels;

public class NurseViewModel
{
	public int Id { get; set; }
	public string? Key { get; set; }
	public string Name { get; set; } = null!;
	public string NationalId { get; set; } = null!;
	public int Age { get; set; }
	public Gender Gender { get; set; }
	public string Email { get; set; } = null!;
	public string MobileNumber { get; set; } = null!;
	public string Address { get; set; } = null!;
	public decimal Salary { get; set; }
	public DateOnly HireDate { get; set; }
	public bool IsDeleted { get; set; }

	public string Doctor { get; set; } = null!;
}