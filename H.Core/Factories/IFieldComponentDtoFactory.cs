using H.Core.Models.LandManagement.Fields;

namespace H.Core.Factories;

public interface IFieldComponentDtoFactory : IFactory<FieldSystemComponentDto>
{
    FieldSystemComponentDto Create();
    
    FieldSystemComponentDto CreateFieldDto(IFieldComponentDto template);
}