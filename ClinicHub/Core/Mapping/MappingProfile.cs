namespace ClinicHub.Core.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Patient, PatientViewModel>();
		CreateMap<PatientFormViewModel, Patient>().ReverseMap();
		CreateMap<Patient, SelectListItem>()
			.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

		CreateMap<ApplicationUser, UserViewModel>();
		CreateMap<ApplicationUser, UserFormViewModel>().ReverseMap();

		CreateMap<Specialty, SpecialtyViewModel>();
		CreateMap<SpecialtyFormViewModel, Specialty>().ReverseMap();
		CreateMap<Specialty, SelectListItem>()
			.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

		CreateMap<DoctorFormViewModel, Doctor>().ReverseMap();
		CreateMap<Doctor, DoctorSearchResultViewModel>();
		CreateMap<Doctor, DoctorViewModel>();
		CreateMap<Doctor, SelectListItem>()
			.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

		CreateMap<Nurse, NurseViewModel>()
			.ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor.Name));
		CreateMap<NurseFormViewModel, Nurse>().ReverseMap();

		CreateMap<DoctorSchedule, SelectListItem>()
			.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.DayOfWeek));

		CreateMap<Appointment, AppointmentViewModel>()
			.ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient!.Name))
			.ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor!.Name))
			.ForMember(dest => dest.TimeSlot, opt => opt.MapFrom(src => DateTime.Today.Add(src.AppointmentTime).ToString("hh:mm tt")))
			.ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate.ToString("dd, MMM, yyyy")));
	}
}