﻿using H.Avalonia.ViewModels.ComponentViews.OtherAnimals;
using H.Core;
using H.Core.Enumerations;
using H.Core.Models;
using H.Core.Services.Animals;
using H.Core.Services.StorageService;
using Microsoft.Extensions.Logging;
using Moq;

namespace H.Avalonia.Test.ViewModels.ComponentViews.OtherAnimals
{
    [TestClass]
    public class MulesComponentViewModelTests
    {
        private MulesComponentViewModel _viewModel;
        private Mock<IStorageService> _mockStorageService;
        private IStorageService _storageServiceMock;
        private Mock<IStorage> _mockStorage;
        private IStorage _storageMock;
        private ApplicationData _applicationData;
        private Mock<IAnimalComponentService> _mockAnimalComponentService;

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
            var mockLogger = new Mock<ILogger>();
            _mockAnimalComponentService = new Mock<IAnimalComponentService>();

            _viewModel = new MulesComponentViewModel(mockLogger.Object, _storageServiceMock, _mockAnimalComponentService.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void TestConstructorSettingViewName()
        {
            string expectedName = "Mules";
            Assert.AreEqual(expectedName, _viewModel.ViewName);
        }

        [TestMethod]
        public void TestConstructorSettingAnimalType()
        {
            AnimalType expectedAnimalType = AnimalType.Mules;
            Assert.AreEqual(expectedAnimalType, _viewModel.OtherAnimalType);
        }
    }
}