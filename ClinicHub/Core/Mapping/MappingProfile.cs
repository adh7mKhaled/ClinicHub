﻿using Microsoft.AspNetCore.Mvc.Rendering;

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
		CreateMap<Specialty, SelectListItem>()
			.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

		CreateMap<DoctorFormViewModel, Doctor>().ReverseMap();
		CreateMap<Doctor, DoctorSearchResultViewModel>();
		CreateMap<Doctor, DoctorViewModel>();
	}
}