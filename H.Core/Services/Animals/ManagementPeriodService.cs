using H.Core.Enumerations;
using H.Core.Models;

namespace H.Core.Services.Animals;

public class ManagementPeriodService : IManagementPeriodService
{
    #region Public Methods

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