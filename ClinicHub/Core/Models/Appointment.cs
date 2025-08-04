namespace ClinicHub.Core.Models;

public class Appointment
{
	public int Id { get; set; }
	public int DoctorId { get; set; }
	public int PatientId { get; set; }
	public DateOnly AppointmentDate { get; set; }
	public TimeSpan AppointmentTime { get; set; }
}