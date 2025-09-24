using AutoMapper;
using H.Avalonia.ViewModels.ComponentViews;
using H.Core.Factories;
using H.Core.Mappers;
using H.Core.Models.Animals;
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

        _mockContainerProvider.Setup(x => x.Resolve(typeof(IMapper), nameof(ManagementPeriodViewItemToManagementPeriodDtoMapper))).Returns(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ManagementPeriodViewItemToManagementPeriodDtoMapper>();
        }).CreateMapper());

        _mockContainerProvider.Setup(x => x.Resolve(typeof(IMapper), nameof(ManagementPeriodDtoToManagementPeriodViewItemMapper))).Returns(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ManagementPeriodDtoToManagementPeriodViewItemMapper>();
        }).CreateMapper());

        _mockContainerProvider.Setup(x => x.Resolve(typeof(IMapper), nameof(ManagementPeriodToManagementPeriodDtoMapper))).Returns(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ManagementPeriodToManagementPeriodDtoMapper>();
        }).CreateMapper());

        _mockContainerProvider.Setup(x => x.Resolve(typeof(IMapper), nameof(ManagementPeriodDtoToManagementPeriodMapper))).Returns(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ManagementPeriodDtoToManagementPeriodMapper>();
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
        Assert.AreEqual("New Management Period", dto.Name);
        Assert.IsTrue(dto.NumberOfDays > 0);
    }

    [TestMethod]
    public void CreateManagementPeriodDto_FromTemplate_ReturnsNewInstanceWithSameValues()
    {
        // Arrange
        var template = new ManagementPeriodDto();
        template.Name = "Test Period";
        template.StartDate = new DateTime(2024, 1, 1);
        template.EndDate = new DateTime(2024, 12, 31);
        template.NumberOfDays = 366;

        // Act
        var dto = _factory.CreateManagementPeriodDto(template);

        // Assert
        Assert.IsNotNull(dto);
        Assert.IsInstanceOfType(dto, typeof(IManagementPeriodDto));
        Assert.AreEqual("Test Period", dto.Name);
        Assert.AreEqual(template.StartDate, dto.StartDate);
        Assert.AreEqual(template.EndDate, dto.EndDate);
        Assert.AreEqual(template.NumberOfDays, dto.NumberOfDays);
        Assert.AreNotSame(template, dto); // Should be a different instance
    }

    [TestMethod]
    public void CreateManagementPeriodDto_FromViewItem_ReturnsNewInstanceWithSameValues()
    {
        // Arrange
        var viewItem = new ManagementPeriodViewItem();
        viewItem.Name = "Test ViewItem Period";
        viewItem.StartDate = new DateTime(2024, 6, 1);
        viewItem.EndDate = new DateTime(2024, 8, 31);
        
        // Act
        var dto = _factory.CreateManagementPeriodDto(viewItem);

        // Assert
        Assert.IsNotNull(dto);
        Assert.IsInstanceOfType(dto, typeof(IManagementPeriodDto));
        Assert.AreEqual("Test ViewItem Period", dto.Name);
        Assert.AreEqual(viewItem.StartDate, dto.StartDate);
        Assert.AreEqual(viewItem.EndDate, dto.EndDate);
        Assert.AreEqual(viewItem.NumberOfDays, dto.NumberOfDays);
    }

    [TestMethod]
    public void CreateManagementPeriodDto_FromDomainModel_ReturnsNewInstanceWithSameValues()
    {
        // Arrange
        var managementPeriod = new ManagementPeriod();
        managementPeriod.Name = "Test Domain Period";
        managementPeriod.Start = new DateTime(2024, 3, 1);
        managementPeriod.End = new DateTime(2024, 5, 31);
        managementPeriod.NumberOfDays = 92;

        // Act
        var dto = _factory.CreateManagementPeriodDto(managementPeriod);

        // Assert
        Assert.IsNotNull(dto);
        Assert.IsInstanceOfType(dto, typeof(IManagementPeriodDto));
        Assert.AreEqual("Test Domain Period", dto.Name);
        Assert.AreEqual(managementPeriod.Start, dto.StartDate);
        Assert.AreEqual(managementPeriod.End, dto.EndDate);
        Assert.AreEqual(managementPeriod.NumberOfDays, dto.NumberOfDays);
    }

    [TestMethod]
    public void CreateManagementPeriodViewItem_FromDto_ReturnsNewViewItemWithSameValues()
    {
        // Arrange
        var dto = new ManagementPeriodDto();
        dto.Name = "Test DTO Period";
        dto.StartDate = new DateTime(2024, 7, 1);
        dto.EndDate = new DateTime(2024, 9, 30);
        dto.NumberOfDays = 92;

        // Act
        var viewItem = _factory.CreateManagementPeriodViewItem(dto);

        // Assert
        Assert.IsNotNull(viewItem);
        Assert.IsInstanceOfType(viewItem, typeof(ManagementPeriodViewItem));
        Assert.AreEqual("Test DTO Period", viewItem.Name);
        Assert.AreEqual(dto.StartDate, viewItem.StartDate);
        Assert.AreEqual(dto.EndDate, viewItem.EndDate);
        Assert.AreEqual(dto.NumberOfDays, viewItem.NumberOfDays);
    }

    [TestMethod]
    public void CreateManagementPeriod_FromDto_ReturnsNewDomainModelWithSameValues()
    {
        // Arrange
        var dto = new ManagementPeriodDto();
        dto.Name = "Test DTO to Domain Period";
        dto.StartDate = new DateTime(2024, 4, 1);
        dto.EndDate = new DateTime(2024, 6, 30);
        dto.NumberOfDays = 91;

        // Act
        var managementPeriod = _factory.CreateManagementPeriod(dto);

        // Assert
        Assert.IsNotNull(managementPeriod);
        Assert.IsInstanceOfType(managementPeriod, typeof(ManagementPeriod));
        Assert.AreEqual("Test DTO to Domain Period", managementPeriod.Name);
        Assert.AreEqual(dto.StartDate, managementPeriod.Start);
        Assert.AreEqual(dto.EndDate, managementPeriod.End);
        Assert.AreEqual(dto.NumberOfDays, managementPeriod.NumberOfDays);
    }
}