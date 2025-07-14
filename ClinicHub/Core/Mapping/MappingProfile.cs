namespace ClinicHub.Core.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Patient, PatientViewModel>();
		CreateMap<PatientFormViewModel, Patient>().ReverseMap();

		CreateMap<ApplicationUser, UserViewModel>();
		CreateMap<ApplicationUser, UserFormViewModel>().ReverseMap();

		CreateMap<Specialty, SpecialtyViewModel>();
		CreateMap<SpecialtyFormViewModel, Specialty>().ReverseMap();
	}
}