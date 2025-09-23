using H.Core.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.Test.Factories;

[TestClass]
public class AnimalComponentFactoryTests
{
    [TestMethod]
    public void CreateAnimalComponentDto_ReturnsNonNullInstance()
    {
        // Arrange
        var factory = new AnimalComponentFactory();
        var inputDto = new AnimalComponentDto();

        // Act
        var dto = factory.CreateAnimalComponentDto(inputDto);

        // Assert
        Assert.IsNotNull(dto);
    }

    [TestMethod]
    public void CreateAnimalComponentDto_ReturnsNewInstanceEachTime()
    {
        // Arrange
        var factory = new AnimalComponentFactory();
        var inputDto = new AnimalComponentDto();

        // Act
        var dto1 = factory.CreateAnimalComponentDto(inputDto);
        var dto2 = factory.CreateAnimalComponentDto(inputDto);

        // Assert
        Assert.AreNotSame(dto1, dto2);
    }

    [TestMethod]
    public void CreateAnimalComponentDto_ImplementsIAnimalComponentDto()
    {
        // Arrange
        var factory = new AnimalComponentFactory();
        var inputDto = new AnimalComponentDto();

        // Act
        var dto = factory.CreateAnimalComponentDto(inputDto);

        // Assert
        Assert.IsInstanceOfType(dto, typeof(IAnimalComponentDto));
    }
}