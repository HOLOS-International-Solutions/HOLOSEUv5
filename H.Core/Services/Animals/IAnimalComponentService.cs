using CsvHelper;
using H.Core.Factories;
using H.Core.Models;
using H.Core.Models.Animals;

namespace H.Core.Services.Animals;

public interface IAnimalComponentService
{
    public void InitializeAnimalComponent(Farm farm, AnimalComponentBase animalComponent);
    public IAnimalComponentDto TransferToAnimalComponentDto(AnimalComponentBase animalComponent);
    public IAnimalComponentDto TransferToAnimalComponentDtoToSystem(IAnimalComponentDto animalComponentDto, AnimalComponentBase animalComponent);
}