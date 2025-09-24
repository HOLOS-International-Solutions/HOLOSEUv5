using AutoMapper;
using H.Core.Factories;
using H.Core.Models.Animals;

namespace H.Core.Mappers;

public class ManagementPeriodDtoToManagementPeriodViewItemMapper : Profile
{
    public ManagementPeriodDtoToManagementPeriodViewItemMapper()
    {
        CreateMap<IManagementPeriodDto, ManagementPeriodViewItem>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.NumberOfDays));
    }
}