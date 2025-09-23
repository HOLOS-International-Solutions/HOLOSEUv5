namespace H.Core.Factories;

public class AnimalComponentFactory : IAnimalComponentFactory
{
    #region Public Methods
    
    public IAnimalComponentDto CreateAnimalComponentDto(IAnimalComponentDto animalComponentDto)
    {
        var result = new AnimalComponentDto();

        return result;
    } 

    #endregion
}