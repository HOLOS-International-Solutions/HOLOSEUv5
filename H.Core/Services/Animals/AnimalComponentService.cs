using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Converters;
using H.Core.Factories;
using H.Core.Models;
using H.Core.Models.Animals;
using Microsoft.Extensions.Logging;
using Prism.Ioc;

namespace H.Core.Services.Animals;

public class AnimalComponentService : ComponentServiceBase, IAnimalComponentService
{
    #region Fields

    private readonly IAnimalComponentFactory _animalComponentFactory;

    #endregion

    #region Constructors

    public AnimalComponentService(ILogger logger, IContainerProvider containerProvider, IAnimalComponentFactory animalComponentFactory, IUnitsOfMeasurementCalculator unitsOfMeasurementCalculator) : base(logger, containerProvider, unitsOfMeasurementCalculator)
    {
        if (animalComponentFactory != null)
        {
            _animalComponentFactory = animalComponentFactory;
        }
        else
        {
            throw new ArgumentNullException(nameof(animalComponentFactory));
        }
    }

    #endregion

    #region Public Methods

    public void InitializeAnimalComponent(Farm farm, AnimalComponentBase animalComponent)
    {
        if (animalComponent.IsInitialized)
        {
            return;
        }

        animalComponent.IsInitialized = true;
        animalComponent.Name = base.GetUniqueComponentName(farm, animalComponent);
    }

    public IAnimalComponentDto TransferToAnimalComponentDto(AnimalComponentBase animalComponent)
    {
        var result = new AnimalComponentDto();

        return result;
    }

    public IAnimalComponentDto TransferToAnimalComponentDtoToSystem(IAnimalComponentDto animalComponentDto, AnimalComponentBase animalComponent)
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

        return copy;
    }

    #endregion
}