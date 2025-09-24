using System.Collections.Generic;
using H.Core.Calculators.UnitsOfMeasurement;
using H.Core.Enumerations;
using H.Core.Factories;
using H.Core.Models.Animals;
using H.Core.Services.Animals;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Ioc;

namespace H.Core.Test.Services.Animals
{
    [TestClass]
    public class ManagementPeriodServiceTests
    {
        private ManagementPeriodService _service;
        private Mock<ILogger> _mockLogger;
        private Mock<IContainerProvider> _mockContainerProvider;
        private Mock<IManagementPeriodFactory> _mockManagementPeriodFactory;
        private Mock<IUnitsOfMeasurementCalculator> _mockUnitsOfMeasurementCalculator;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger>();
            _mockContainerProvider = new Mock<IContainerProvider>();
            _mockManagementPeriodFactory = new Mock<IManagementPeriodFactory>();
            _mockUnitsOfMeasurementCalculator = new Mock<IUnitsOfMeasurementCalculator>();
            
            // For the existing bedding material tests, we can create a simplified service 
            // that doesn't require the new dependencies
            _service = new ManagementPeriodService(_mockLogger.Object, _mockContainerProvider.Object, 
                _mockManagementPeriodFactory.Object, _mockUnitsOfMeasurementCalculator.Object);
        }

        [TestMethod]
        public void GetValidBeddingMaterialTypes_BeefProduction_ReturnsExpectedTypes()
        {
            var result = _service.GetValidBeddingMaterialTypes(AnimalType.Beef);

            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.None);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.WoodChip);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Straw);
        }

        [TestMethod]
        public void GetValidBeddingMaterialTypes_Dairy_ReturnsExpectedTypes()
        {
            var result = _service.GetValidBeddingMaterialTypes(AnimalType.Dairy);

            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.None);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Sand);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.SeparatedManureSolid);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.StrawLong);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.StrawChopped);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Shavings);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Sawdust);
        }

        [TestMethod]
        public void GetValidBeddingMaterialTypes_Swine_ReturnsExpectedTypes()
        {
            var result = _service.GetValidBeddingMaterialTypes(AnimalType.Swine);

            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.None);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.StrawLong);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.StrawChopped);
        }

        [TestMethod]
        public void GetValidBeddingMaterialTypes_Sheep_ReturnsExpectedTypes()
        {
            var result = _service.GetValidBeddingMaterialTypes(AnimalType.Sheep);

            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.None);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Straw);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Shavings);
        }

        [TestMethod]
        public void GetValidBeddingMaterialTypes_Poultry_ReturnsExpectedTypes()
        {
            var result = _service.GetValidBeddingMaterialTypes(AnimalType.Poultry);

            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.None);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Straw);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Shavings);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Sawdust);
        }

        [TestMethod]
        public void GetValidBeddingMaterialTypes_OtherLivestock_ReturnsExpectedTypes()
        {
            var result = _service.GetValidBeddingMaterialTypes(AnimalType.OtherLivestock);

            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.None);
            CollectionAssert.Contains((List<BeddingMaterialType>)result, BeddingMaterialType.Straw);
        }
    }
}