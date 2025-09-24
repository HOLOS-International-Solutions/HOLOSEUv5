using AutoMapper;
using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Converters;
using H.Core.Enumerations;
using H.Core.Factories;
using H.Core.Mappers;
using H.Core.Models;
using H.Core.Models.Animals;
using H.Core.Services;
using Microsoft.Extensions.Logging;
using Prism.Ioc;

namespace H.Core.Services.Animals;

public class ManagementPeriodService : ComponentServiceBase, IManagementPeriodService
{
    #region Fields

    private readonly IManagementPeriodFactory _managementPeriodFactory;

    private readonly IMapper _managementPeriodDtoToManagementPeriodMapper;
    private readonly IMapper _managementPeriodToManagementPeriodDtoMapper;

    #endregion

    #region Constructors

    public ManagementPeriodService(ILogger logger, IContainerProvider containerProvider, IManagementPeriodFactory managementPeriodFactory, IUnitsOfMeasurementCalculator unitsOfMeasurementCalculator) : base(logger, containerProvider, unitsOfMeasurementCalculator)
    {
        if (managementPeriodFactory != null)
        {
            _managementPeriodFactory = managementPeriodFactory;
        }
        else
        {
            throw new ArgumentNullException(nameof(managementPeriodFactory));
        }

        _managementPeriodDtoToManagementPeriodMapper = base.ContainerProvider.Resolve<IMapper>(nameof(ManagementPeriodDtoToManagementPeriodMapper));
        _managementPeriodToManagementPeriodDtoMapper = base.ContainerProvider.Resolve<IMapper>(nameof(ManagementPeriodToManagementPeriodDtoMapper));
    }

    #endregion

    #region Public Methods

    public IManagementPeriodDto TransferToManagementPeriodDto(ManagementPeriod managementPeriod)
    {
        var managementPeriodDto = new ManagementPeriodDto();

        // Create a copy of the component by copying all properties into the DTO
        _managementPeriodToManagementPeriodDtoMapper.Map(managementPeriod, managementPeriodDto);

        // All numerical values are stored internally as metric values
        var propertyConverter = new PropertyConverter<IManagementPeriodDto>(managementPeriodDto);

        // Get all properties that might need to be converted to imperial units before being shown to the user
        foreach (var property in propertyConverter.PropertyInfos)
        {
            // Convert the value from metric to imperial as needed. Note the converter won't convert anything if the display is in metric units
            var bindingValue = propertyConverter.GetBindingValueFromSystem(property, base.UnitsOfMeasurementCalculator.GetUnitsOfMeasurement());

            // Set the value of the property before displaying to the user
            property.SetValue(managementPeriodDto, bindingValue);
        }

        return managementPeriodDto;
    }

    public IManagementPeriodDto TransferManagementPeriodDtoToSystem(IManagementPeriodDto managementPeriodDto, ManagementPeriod managementPeriod)
    {
        // Create a copy of the DTO since we don't want to change values on the original that is still bound to the GUI
        var copy = _managementPeriodFactory.CreateManagementPeriodDto(managementPeriodDto);

        var propertyConverter = new PropertyConverter<IManagementPeriodDto>(copy);

        // Get all properties that might need to be converted to imperial units before being shown to the user
        foreach (var property in propertyConverter.PropertyInfos)
        {
            // Convert the value from imperial to metric as needed (no conversion will occur if display is using metric)
            var bindingValue = propertyConverter.GetSystemValueFromBinding(property, base.UnitsOfMeasurementCalculator.GetUnitsOfMeasurement());

            // Set the value on the copy of the DTO
            property.SetValue(copy, bindingValue);
        }

        // Map values from the copy of the DTO to the internal system object
        _managementPeriodDtoToManagementPeriodMapper.Map(copy, managementPeriod);

        return copy;
    }

    public IReadOnlyCollection<BeddingMaterialType> GetValidBeddingMaterialTypes(AnimalType animalType)
    {
        var result = new List<BeddingMaterialType>();
        result.Add(BeddingMaterialType.None);

        var category = animalType.GetComponentCategoryFromAnimalType();
        switch (category)
        {
            case ComponentCategory.BeefProduction:
                {
                    result.Add(BeddingMaterialType.WoodChip);
                    result.Add(BeddingMaterialType.Straw);
                }
                break;

            case ComponentCategory.Dairy:
                {
                    result.Add(BeddingMaterialType.Sand);
                    result.Add(BeddingMaterialType.SeparatedManureSolid);
                    result.Add(BeddingMaterialType.StrawLong);
                    result.Add(BeddingMaterialType.StrawChopped);
                    result.Add(BeddingMaterialType.Shavings);
                    result.Add(BeddingMaterialType.Sawdust);
                }
                break;

            case ComponentCategory.Swine:
                {
                    result.Add(BeddingMaterialType.StrawLong);
                    result.Add(BeddingMaterialType.StrawChopped);
                }
                break;

            case ComponentCategory.Sheep:
                {
                    result.Add(BeddingMaterialType.Straw);
                    result.Add(BeddingMaterialType.Shavings);
                }
                break;

            case ComponentCategory.Poultry:
                {
                    result.Add(BeddingMaterialType.Straw);
                    result.Add(BeddingMaterialType.Shavings);
                    result.Add(BeddingMaterialType.Sawdust);
                }
                break;

            case ComponentCategory.OtherLivestock:
                {
                    result.Add(BeddingMaterialType.Straw);
                }
                break;
        }

        return result;
    }

    #endregion
}