namespace ClinicHub.Core.ViewModels;

public class AppointmentReportViewModel
{
    public List<int>? SelectedDoctorsIDs { get; set; }
    public IEnumerable<SelectListItem> Doctors { get; set; } = [];

    public List<int>? SelectedPatientsIDs { get; set; }
    public IEnumerable<SelectListItem> Patients { get; set; } = [];

    public PaginatedList<Appointment>? Appointments { get; set; }
}