using H.Core.Calculators.Infrastructure;
using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Emissions.Results;
using H.Core.Enumerations;
using H.Core.Factories;
using H.Core.Models;
using H.Core.Models.Animals;
using H.Core.Models.Infrastructure;
using H.Core.Models.LandManagement.Fields;
using H.Core.Services.Animals;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Ioc;

namespace H.Core.Test.Services.Animals;

[TestClass]
public class AnimalComponentServiceTests
{
    #region Fields

    private AnimalComponentService _sut;

    #endregion

    #region Initialization

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
        var mockAnimalComponentFactory = new Mock<IAnimalComponentFactory>();
        var mockLogger = new Mock<ILogger>();
        var mockContainerProvider = new Mock<IContainerProvider>();
        var mockUnitsOfMeasurementCalculator = new Mock<IUnitsOfMeasurementCalculator>();

        _sut = new AnimalComponentService(mockLogger.Object, mockContainerProvider.Object, mockAnimalComponentFactory.Object, mockUnitsOfMeasurementCalculator.Object);
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }

    #endregion

    [TestMethod]
    public void InitializeAnimalComponent_SetsIsInitialized_WhenNotAlreadyInitialized()
    {
        // Arrange
        var farm = new Farm();
        var mockAnimalComponent = new Mock<AnimalComponentBase>();
        mockAnimalComponent.CallBase = true;
        mockAnimalComponent.Object.IsInitialized = false;

        // Act
        _sut.InitializeAnimalComponent(farm, mockAnimalComponent.Object);

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

        // Act
        _sut.InitializeAnimalComponent(farm, mockAnimalComponent.Object);

        // Assert
        Assert.IsTrue(mockAnimalComponent.Object.IsInitialized); // Remains true
    }

    [TestMethod]
    public void TransferToAnimalComponentDtoToSystem_CallsFactoryAndReturnsCopy()
    {
        // Arrange
        var mockFactory = new Mock<IAnimalComponentFactory>();
        var mockLogger = new Mock<ILogger>();
        var mockContainerProvider = new Mock<IContainerProvider>();
        var mockUnitsOfMeasurementCalculator = new Mock<IUnitsOfMeasurementCalculator>();

        var service = new AnimalComponentService(
            mockLogger.Object,
            mockContainerProvider.Object,
            mockFactory.Object,
            mockUnitsOfMeasurementCalculator.Object);

        var originalDto = new Mock<IAnimalComponentDto>().Object;
        var mockAnimalComponent = new Mock<AnimalComponentBase>();
        mockAnimalComponent.CallBase = true;

        var animalComponent = mockAnimalComponent.Object;
        var expectedCopy = new Mock<IAnimalComponentDto>().Object;

        mockFactory
            .Setup(f => f.CreateAnimalComponentDto(originalDto))
            .Returns(expectedCopy);

        // Act
        var result = service.TransferToAnimalComponentDtoToSystem(originalDto, animalComponent);

        // Assert
        Assert.AreSame(expectedCopy, result);
        mockFactory.Verify(f => f.CreateAnimalComponentDto(originalDto), Times.Once);
    }
}