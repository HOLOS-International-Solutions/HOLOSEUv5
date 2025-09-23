using H.Core.Models;
using H.Core.Models.Animals;
using H.Core.Services.Animals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace H.Core.Test.Services.Animals;

[TestClass]
public class AnimalComponentServiceTests
{
    [TestMethod]
    public void InitializeAnimalComponent_SetsIsInitialized_WhenNotAlreadyInitialized()
    {
        // Arrange
        var farm = new Farm();
        var mockAnimalComponent = new Mock<AnimalComponentBase>();
        mockAnimalComponent.CallBase = true;
        mockAnimalComponent.Object.IsInitialized = false;

        var service = new AnimalComponentService();

        // Act
        service.InitializeAnimalComponent(farm, mockAnimalComponent.Object);

        // Assert
        Assert.IsTrue(mockAnimalComponent.Object.IsInitialized);
    }

    [TestMethod]
    public void InitializeAnimalComponent_DoesNotSetIsInitialized_WhenAlreadyInitialized()
    {
        // Arrange
        var farm = new Farm();
        var mockAnimalComponent = new Mock<AnimalComponentBase>();
        mockAnimalComponent.CallBase = true;
        mockAnimalComponent.Object.IsInitialized = true;

        var service = new AnimalComponentService();

        // Act
        service.InitializeAnimalComponent(farm, mockAnimalComponent.Object);

        // Assert
        Assert.IsTrue(mockAnimalComponent.Object.IsInitialized); // Remains true
    }
}