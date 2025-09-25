using AutoMapper;
using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Converters;
using H.Core.Factories;
using H.Core.Models.Animals;
using H.Infrastructure;

namespace H.Core.Services.Animals
{
    public class TransferService<TModelBase, TDto> : ITransferService<TModelBase, TDto>
        where TModelBase : ModelBase
        where TDto : IDto, new()
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IUnitsOfMeasurementCalculator _unitsOfMeasurementCalculator;

        #endregion

        #region Constructors

        public TransferService(IUnitsOfMeasurementCalculator unitsOfMeasurementCalculator, Action<IMapperConfigurationExpression> additionalConfig = null)
        {
            _unitsOfMeasurementCalculator = unitsOfMeasurementCalculator;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModelBase, TDto>();
                additionalConfig?.Invoke(cfg);
            });

            _mapper = config.CreateMapper();
        }

        #endregion

        #region Public Methods

        public TDto TransferToDto(TModelBase model)
        {
            var dto = new TDto();

            // Use the internal mapper
            _mapper.Map(model, dto);

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

        #endregion
    }
}