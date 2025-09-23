using H.Core.Factories;
using H.Core.Models;
using H.Core.Models.Animals;

namespace H.Core.Services.Animals;

public class AnimalComponentService : ComponentServiceBase, IAnimalComponentService
{
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
        throw new NotImplementedException();
    } 

    #endregion
}