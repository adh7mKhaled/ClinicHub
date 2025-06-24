namespace ClinicHub.Core.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Patient, PatientViewModel>()
			.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
	}
}