﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Avalonia.ViewModels.OptionsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Core.Models;
using H.Core.Providers.Temperature;
using H.Core.Services.StorageService;
using H.Core;
using Moq;

namespace H.Avalonia.ViewModels.OptionsViews.Tests
{
    [TestClass]
    public class BarnTemperatureSettingsViewModelTests
    {
        private Farm _testFarm;
        private TemperatureData _temperatureData;
        private BarnTemperatureSettingsViewModel _viewModel;
        private Mock<IStorageService> _mockStorageService;
        private IStorageService _storageServiceMock;
        private Mock<IStorage> _mockStorage;
        private IStorage _storageMock;
        private ApplicationData _applicationData;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStorageService = new Mock<IStorageService>();
            _storageServiceMock = _mockStorageService.Object;
            _mockStorage = new Mock<IStorage>();
            _storageMock = _mockStorage.Object;

            _applicationData = new ApplicationData();
            _mockStorage.Setup(x => x.ApplicationData).Returns(_applicationData);
            _mockStorageService.Setup(x => x.Storage).Returns(_storageMock);

            _testFarm = new Farm();
            _temperatureData = new TemperatureData
            {
                January = -4.2,
                February = 8.6,
                March = 7.3,
                April = -9.0,
                May = 4.7,
                June = 5.4,
                July = -0.8,
                August = 6.1,
                September = 3.5,
                October = -13.6,
                November = -2.0,
                December = 1.0
            };
            _testFarm.ClimateData.BarnTemperatureData = _temperatureData;
            _mockStorageService.Setup(x => x.GetActiveFarm()).Returns(_testFarm);
        }

        [TestMethod]
        public void TestInitializationLogic()
        {
            _viewModel = new BarnTemperatureSettingsViewModel(_storageServiceMock);

            Assert.AreEqual(-4.2, _viewModel.Data.January);
            Assert.AreEqual(8.6, _viewModel.Data.February);
            Assert.AreEqual(7.3, _viewModel.Data.March);
            Assert.AreEqual(-9.0, _viewModel.Data.April);
            Assert.AreEqual(4.7, _viewModel.Data.May);
            Assert.AreEqual(5.4, _viewModel.Data.June);
            Assert.AreEqual(-0.8, _viewModel.Data.July);
            Assert.AreEqual(6.1, _viewModel.Data.August);
            Assert.AreEqual(3.5, _viewModel.Data.September);
            Assert.AreEqual(-13.6, _viewModel.Data.October);
            Assert.AreEqual(-2.0, _viewModel.Data.November);
            Assert.AreEqual(1.0, _viewModel.Data.December);
        }
    }
}