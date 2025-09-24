using AutoMapper;
using H.Avalonia.ViewModels.ComponentViews;
using H.Core.Factories;
using H.Core.Mappers;
using H.Core.Services.Initialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Ioc;
using System;
using IManagementPeriodDto = H.Core.Factories.IManagementPeriodDto;

namespace H.Core.Test.Factories;

[TestClass]
public class ManagementPeriodFactoryTests
{
    private Mock<IContainerProvider> _mockContainerProvider;
    private ManagementPeriodFactory _factory;

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
        _mockContainerProvider = new Mock<IContainerProvider>();

        // Setup mappers to return a working IMapper for each required profile
        _mockContainerProvider.Setup(x => x.Resolve(typeof(IMapper), nameof(ManagementPeriodDtoToManagementPeriodDtoMapper))).Returns(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ManagementPeriodDtoToManagementPeriodDtoMapper>();
        }).CreateMapper());

        _factory = new ManagementPeriodFactory(_mockContainerProvider.Object);
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }

    #endregion

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_WithNullContainerProvider_ThrowsArgumentNullException()
    {
        // Act
        var factory = new ManagementPeriodFactory(null);
    }

    [TestMethod]
    public void CreateManagementPeriodDto_ReturnsNewInstance()
    {
        // Act
        var dto = _factory.CreateManagementPeriodDto();

        // Assert
        Assert.IsNotNull(dto);
        Assert.IsInstanceOfType(dto, typeof(IManagementPeriodDto));
    }
}