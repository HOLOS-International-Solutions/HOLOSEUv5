using System.Collections.Generic;
using H.Core.Enumerations;
using H.Core.Services.Animals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.Test.Services.Animals
{
    [TestClass]
    public class ManagementPeriodServiceTests
    {
        private ManagementPeriodService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new ManagementPeriodService();
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