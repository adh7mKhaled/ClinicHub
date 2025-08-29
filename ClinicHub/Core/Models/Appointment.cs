namespace ClinicHub.Core.Models;

public class Appointment
{
	public int Id { get; set; }
	public int DoctorId { get; set; }
	public Doctor? Doctor { get; set; }
	public int PatientId { get; set; }
	public Patient? Patient { get; set; }
	public DateOnly AppointmentDate { get; set; }
	public TimeSpan AppointmentTime { get; set; }
}