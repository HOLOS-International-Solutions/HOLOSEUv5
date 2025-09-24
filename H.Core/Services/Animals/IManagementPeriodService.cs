using H.Core.Enumerations;

namespace H.Core.Services.Animals;

public interface IManagementPeriodService
{
    IReadOnlyCollection<BeddingMaterialType> GetValidBeddingMaterialTypes(AnimalType animalType);
}