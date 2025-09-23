using AutoMapper;
using H.Core.Mappers;
using Prism.Ioc;

namespace H.Core.Factories;

public class AnimalComponentFactory : IAnimalComponentFactory
{

    private readonly IMapper? _animalComponentDtoToAnimalComponentDtoMapper;


    public AnimalComponentFactory(IContainerProvider containerProvider)
    {
        if (containerProvider != null)
        {
            _animalComponentDtoToAnimalComponentDtoMapper = containerProvider.Resolve<IMapper>(nameof(AnimalComponentDtoToAnimalComponentDtoMapper));
        }
    }

    #region Public Methods

    public IAnimalComponentDto CreateAnimalComponentDto(IAnimalComponentDto animalComponentDto)
    {
        var result = new AnimalComponentDto();

        _animalComponentDtoToAnimalComponentDtoMapper.Map(animalComponentDto, result);
        
        return result;
    } 

    #endregion
}