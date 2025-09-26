using AutoMapper;
using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Converters;
using H.Core.Factories;
using H.Core.Mappers;
using H.Core.Models;
using H.Core.Models.Animals;
using Microsoft.Extensions.Logging;
using Prism.Ioc;

namespace H.Core.Services.Animals;

public class AnimalComponentService : ComponentServiceBase, IAnimalComponentService
{
    #region Fields

    private readonly IAnimalComponentFactory _animalComponentFactory;

    private readonly IMapper _animalComponentDtoToAnimalComponentMapper;
    private readonly IMapper _animalComponentToDtoMapper;

    #endregion

    #region Constructors

    public AnimalComponentService(ILogger logger, IContainerProvider containerProvider, IAnimalComponentFactory animalComponentFactory) : base(logger, containerProvider)
    {
        if (animalComponentFactory != null)
        {
            _animalComponentFactory = animalComponentFactory;
        }
        else
        {
            throw new ArgumentNullException(nameof(animalComponentFactory));
        }

        _animalComponentDtoToAnimalComponentMapper = base.ContainerProvider.Resolve<IMapper>(nameof(AnimalComponentDtoToAnimalComponentMapper));
        _animalComponentToDtoMapper = base.ContainerProvider.Resolve<IMapper>(nameof(AnimalComponentBaseToAnimalComponentDtoMapper));
    }

    #endregion

    #region Public Methods

    public IAnimalComponentDto TransferToAnimalComponentDto(AnimalComponentBase animalComponent)
    {
        var animalComponentDto = new AnimalComponentDto();

        // Create a copy of the component by copying all properties into the DTO
        _animalComponentToDtoMapper.Map(animalComponent, animalComponentDto);

        // All numerical values are stored internally as metric values
        var propertyConverter = new PropertyConverter<IAnimalComponentDto>(animalComponentDto);

        // Get all properties that might need to be converted to imperial units before being shown to the user
        foreach (var property in propertyConverter.PropertyInfos)
        {
            // Convert the value from metric to imperial as needed. Note the converter won't convert anything if the display is in metric units
            var bindingValue = propertyConverter.GetBindingValueFromSystem(property, base.UnitsOfMeasurementCalculator.GetUnitsOfMeasurement());

            // Set the value of the property before displaying to the user
            property.SetValue(animalComponentDto, bindingValue);
        }

        return animalComponentDto;
    }

    public IAnimalComponentDto TransferAnimalComponentDtoToSystem(IAnimalComponentDto animalComponentDto, AnimalComponentBase animalComponent)
    {
        // Create a copy of the DTO since we don't want to change values on the original that is still bound to the GUI
        var copy = _animalComponentFactory.CreateAnimalComponentDto(animalComponentDto);

        var propertyConverter = new PropertyConverter<IAnimalComponentDto>(copy);

        // Get all properties that might need to be converted to imperial units before being shown to the user
        foreach (var property in propertyConverter.PropertyInfos)
        {
            // Convert the value from imperial to metric as needed (no conversion will occur if display is using metric)
            var bindingValue = propertyConverter.GetSystemValueFromBinding(property, base.UnitsOfMeasurementCalculator.GetUnitsOfMeasurement());

            // Set the value on the copy of the DTO
            property.SetValue(copy, bindingValue);
        }

        // Map values from the copy of the DTO to the internal system object
        _animalComponentDtoToAnimalComponentMapper.Map(copy, animalComponent);

        return copy;
    }

    #endregion
}