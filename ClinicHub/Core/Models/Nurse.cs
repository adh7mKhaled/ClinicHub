namespace ClinicHub.Core.Models;

public class Nurse : BaseModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string NationalId { get; set; } = null!;
	public int Age { get; set; }
	public Gender Gender { get; set; }
	public string Email { get; set; } = null!;
	public string MobileNumber { get; set; } = null!;
	public string Address { get; set; } = null!;
	public double Salary { get; set; }

	public ICollection<Doctor> Doctors { get; set; } = [];
	public ICollection<DoctorNurse> DoctorNurses { get; set; } = [];
}