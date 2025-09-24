using AutoMapper;
using H.Core.Factories;
using H.Core.Models.Animals;

namespace H.Core.Mappers;

public class ManagementPeriodDtoToManagementPeriodMapper : Profile
{
    public ManagementPeriodDtoToManagementPeriodMapper()
    {
        CreateMap<IManagementPeriodDto, ManagementPeriod>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.NumberOfDays));
    }
}