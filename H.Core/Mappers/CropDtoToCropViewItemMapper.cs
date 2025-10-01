using AutoMapper;
using H.Core.Factories;
using H.Core.Factories.Crops;
using H.Core.Models.LandManagement.Fields;

namespace H.Core.Mappers;

public class CropDtoToCropViewItemMapper : Profile
{
    public CropDtoToCropViewItemMapper()
    {
        CreateMap<ICropDto, CropViewItem>();
    }
}