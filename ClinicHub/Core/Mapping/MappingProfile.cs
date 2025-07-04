﻿namespace ClinicHub.Core.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Patient, PatientViewModel>();
		CreateMap<PatientFormViewModel, Patient>().ReverseMap();
	}
}