using AutoMapper;
using H.Core.Factories;
using H.Core.Models.Animals;

namespace H.Core.Mappers;

public class ManagementPeriodToManagementPeriodDtoMapper : Profile
{
    public ManagementPeriodToManagementPeriodDtoMapper()
    {
        CreateMap<ManagementPeriod, IManagementPeriodDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Start))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.End))
            .ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.NumberOfDays));
    }
}