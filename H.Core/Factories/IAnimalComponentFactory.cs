namespace H.Core.Factories;

public interface IAnimalComponentFactory
{
    IAnimalComponentDto CreateAnimalComponentDto(IAnimalComponentDto animalComponentDto);
}