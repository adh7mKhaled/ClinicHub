namespace ClinicHub.Core.Models;

public class DoctorNurse
{
	public int DoctorId { get; set; }
	public Doctor? Doctor { get; set; }

	public int NurseId { get; set; }
	public Nurse? Nurse { get; set; }
}