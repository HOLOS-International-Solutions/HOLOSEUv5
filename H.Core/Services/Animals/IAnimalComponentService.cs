using H.Core.Models;
using H.Core.Models.Animals;

namespace H.Core.Services.Animals;

public interface IAnimalComponentService
{
    public void InitializeAnimalComponent(Farm farm, AnimalComponentBase animalComponent);
}