namespace ClinicHub.Core.Models;

public class Doctor : BaseModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string NationalId { get; set; } = null!;
	public int Age { get; set; }
	public Gender Gender { get; set; }
	public string Email { get; set; } = null!;
	public string MobileNumber { get; set; } = null!;
	public string Address { get; set; } = null!;
	public decimal Salary { get; set; }

	public int SpecialtyId { get; set; }
	public Specialty Specialty { get; set; } = default!;

	public ICollection<Appointment> Appointments { get; set; } = [];
}