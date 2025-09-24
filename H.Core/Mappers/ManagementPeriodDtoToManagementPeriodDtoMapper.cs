using AutoMapper;
using H.Avalonia.ViewModels.ComponentViews;
using H.Core.Factories;

namespace H.Core.Mappers;

public class ManagementPeriodDtoToManagementPeriodDtoMapper : Profile
{
    public ManagementPeriodDtoToManagementPeriodDtoMapper()
    {
        CreateMap<IManagementPeriodDto, IManagementPeriodDto>();
    }
}