using H.Core.Models;
using H.Core.Models.Animals;

namespace H.Core.Services.Animals;

public class AnimalComponentService : ComponentServiceBase, IAnimalComponentService
{
    public void InitializeAnimalComponent(Farm farm, AnimalComponentBase animalComponent)
    {
        if (animalComponent.IsInitialized)
        {
            return;
        }

        animalComponent.IsInitialized = true;
        animalComponent.Name = base.GetUniqueComponentName(farm, animalComponent);
    }
}