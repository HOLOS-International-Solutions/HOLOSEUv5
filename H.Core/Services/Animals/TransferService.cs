using AutoMapper;
using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Converters;
using H.Core.Factories;
using H.Core.Mappers;
using H.Core.Models.Animals;
using H.Core.Models.LandManagement.Fields;
using H.Infrastructure;

namespace H.Core.Services.Animals
{
    public class TransferService<TModelBase, TDto> : ITransferService<TModelBase, TDto>
        where TModelBase : ModelBase, new()
        where TDto : IDto, new()
    {
        #region Fields

        private readonly IMapper _modelToDtoMapper;
        private readonly IUnitsOfMeasurementCalculator _unitsOfMeasurementCalculator;
        private IFactory<TDto> _dtoFactory;
        private IMapper? _dtoToModelMapper;

        #endregion

        #region Constructors

        public TransferService(IUnitsOfMeasurementCalculator unitsOfMeasurementCalculator, IFactory<TDto> dtoFactory, Action<IMapperConfigurationExpression> additionalModelToDtoConfig = null, Action<IMapperConfigurationExpression> additionalDtoToModelConfig = null)
        {
            if (dtoFactory != null)
            {
                _dtoFactory = dtoFactory;
            }
            else
            {
                throw new ArgumentNullException(nameof(dtoFactory));
            }

            if (unitsOfMeasurementCalculator != null)
            {
                _unitsOfMeasurementCalculator = unitsOfMeasurementCalculator;
            }
            else
            {
                throw new ArgumentNullException(nameof(unitsOfMeasurementCalculator));
            }

            var modelToDtoConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModelBase, TDto>();
                additionalModelToDtoConfig?.Invoke(cfg);
            });

            _modelToDtoMapper = modelToDtoConfiguration.CreateMapper();

            var dtoToModelConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDto, TModelBase>();
                additionalDtoToModelConfig?.Invoke(cfg);
            });

            _dtoToModelMapper = dtoToModelConfiguration.CreateMapper();
        }

        #endregion

        #region Public Methods

        public TDto TransferDomainObjectToDto(TModelBase model)
        {
            var dto = new TDto();

            // Use the internal mapper
            _modelToDtoMapper.Map(model, dto);

            // All numerical values are stored internally as metric values
            var propertyConverter = new PropertyConverter<IDto>(dto);

            // Get all properties that might need to be converted to imperial units before being shown to the user
            foreach (var property in propertyConverter.PropertyInfos)
            {
                // Convert the value from metric to imperial as needed. Note the converter won't convert anything if the display is in metric units
                var bindingValue = propertyConverter.GetBindingValueFromSystem(property, _unitsOfMeasurementCalculator.GetUnitsOfMeasurement());

                // Set the value of the property before displaying to the user
                property.SetValue(dto, bindingValue);
            }

            return dto;
        }

        public TModelBase TransferDtoToDomainObject(TDto dto, TModelBase model)
        {
            // Create a copy of the DTO since we don't want to change values on the original that is still bound to the GUI
            var copy = _dtoFactory.CreateDtoFromDtoTemplate(dto);

            // All numerical values are stored internally as metric values
            var propertyConverter = new PropertyConverter<IDto>(copy);

            // Get all properties that might need to be converted to imperial units before being shown to the user
            foreach (var property in propertyConverter.PropertyInfos)
            {
                // Convert the value from imperial to metric as needed (no conversion will occur if display is using metric)
                var bindingValue = propertyConverter.GetSystemValueFromBinding(property, _unitsOfMeasurementCalculator.GetUnitsOfMeasurement());

                // Set the value on the copy of the DTO
                property.SetValue(copy, bindingValue);
            }

            // Map values from the copy of the DTO to the internal system object
            _dtoToModelMapper.Map(copy, model);

            return new TModelBase();
        }

        #endregion
    }
}