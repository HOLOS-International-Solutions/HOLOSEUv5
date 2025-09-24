using AutoMapper;
using H.Avalonia.ViewModels.ComponentViews;
using H.Core.Factories;
using H.Core.Mappers;
using H.Core.Models.Animals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace H.Core.Test.Mappers;

[TestClass]
public class ManagementPeriodMapperTests
{
    private IMapper _dtoToDtoMapper;
    private IMapper _viewItemToDtoMapper;
    private IMapper _dtoToViewItemMapper;
    private IMapper _domainToDtoMapper;
    private IMapper _dtoToDomainMapper;

    [TestInitialize]
    public void TestInitialize()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ManagementPeriodDtoToManagementPeriodDtoMapper>();
            cfg.AddProfile<ManagementPeriodViewItemToManagementPeriodDtoMapper>();
            cfg.AddProfile<ManagementPeriodDtoToManagementPeriodViewItemMapper>();
            cfg.AddProfile<ManagementPeriodToManagementPeriodDtoMapper>();
            cfg.AddProfile<ManagementPeriodDtoToManagementPeriodMapper>();
        });

        _dtoToDtoMapper = new Mapper(config);
        _viewItemToDtoMapper = new Mapper(config);
        _dtoToViewItemMapper = new Mapper(config);
        _domainToDtoMapper = new Mapper(config);
        _dtoToDomainMapper = new Mapper(config);
    }

    [TestMethod]
    public void ManagementPeriodDtoToManagementPeriodDtoMapper_MapsCorrectly()
    {
        // Arrange
        var source = new ManagementPeriodDto();
        source.Name = "Test Period";
        source.StartDate = new DateTime(2024, 1, 1);
        source.EndDate = new DateTime(2024, 12, 31);
        source.NumberOfDays = 366;

        // Act
        var result = _dtoToDtoMapper.Map<IManagementPeriodDto>(source);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(source.Name, result.Name);
        Assert.AreEqual(source.StartDate, result.StartDate);
        Assert.AreEqual(source.EndDate, result.EndDate);
        Assert.AreEqual(source.NumberOfDays, result.NumberOfDays);
    }

    [TestMethod]
    public void ManagementPeriodViewItemToManagementPeriodDtoMapper_MapsCorrectly()
    {
        // Arrange
        var source = new ManagementPeriodViewItem();
        source.Name = "Test ViewItem Period";
        source.StartDate = new DateTime(2024, 6, 1);
        source.EndDate = new DateTime(2024, 8, 31);
        // NumberOfDays should be calculated automatically

        // Act
        var result = _viewItemToDtoMapper.Map<IManagementPeriodDto>(source);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(source.Name, result.Name);
        Assert.AreEqual(source.StartDate, result.StartDate);
        Assert.AreEqual(source.EndDate, result.EndDate);
        Assert.AreEqual(source.NumberOfDays, result.NumberOfDays);
    }

    [TestMethod]
    public void ManagementPeriodDtoToManagementPeriodViewItemMapper_MapsCorrectly()
    {
        // Arrange
        var source = new ManagementPeriodDto();
        source.Name = "Test DTO Period";
        source.StartDate = new DateTime(2024, 3, 1);
        source.EndDate = new DateTime(2024, 5, 31);
        source.NumberOfDays = 92;

        // Act
        var result = _dtoToViewItemMapper.Map<ManagementPeriodViewItem>(source);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(source.Name, result.Name);
        Assert.AreEqual(source.StartDate, result.StartDate);
        Assert.AreEqual(source.EndDate, result.EndDate);
        Assert.AreEqual(source.NumberOfDays, result.NumberOfDays);
    }

    [TestMethod]
    public void ManagementPeriodToManagementPeriodDtoMapper_MapsCorrectly()
    {
        // Arrange
        var source = new ManagementPeriod();
        source.Name = "Test Domain Period";
        source.Start = new DateTime(2024, 7, 1);
        source.End = new DateTime(2024, 9, 30);
        source.NumberOfDays = 92;

        // Act
        var result = _domainToDtoMapper.Map<IManagementPeriodDto>(source);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(source.Name, result.Name);
        Assert.AreEqual(source.Start, result.StartDate);
        Assert.AreEqual(source.End, result.EndDate);
        Assert.AreEqual(source.NumberOfDays, result.NumberOfDays);
    }

    [TestMethod]
    public void ManagementPeriodDtoToManagementPeriodMapper_MapsCorrectly()
    {
        // Arrange
        var source = new ManagementPeriodDto();
        source.Name = "Test DTO to Domain Period";
        source.StartDate = new DateTime(2024, 4, 1);
        source.EndDate = new DateTime(2024, 6, 30);
        source.NumberOfDays = 91;

        // Act
        var result = _dtoToDomainMapper.Map<ManagementPeriod>(source);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(source.Name, result.Name);
        Assert.AreEqual(source.StartDate, result.Start);
        Assert.AreEqual(source.EndDate, result.End);
        Assert.AreEqual(source.NumberOfDays, result.NumberOfDays);
    }
}