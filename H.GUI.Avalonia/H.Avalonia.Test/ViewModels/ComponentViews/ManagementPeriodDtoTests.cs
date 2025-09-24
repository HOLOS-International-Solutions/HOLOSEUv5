using H.Avalonia.ViewModels.ComponentViews;
using H.Core.Factories;

namespace H.Avalonia.Test.ViewModels.ComponentViews
{
    [TestClass]
    public class ManagementPeriodDtoTests
    {
        private ManagementPeriodDto _dto;

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
            _dto = new ManagementPeriodDto();
            _dto.StartDate = new DateTime(2000, 01, 01);
            _dto.EndDate = new DateTime(2010, 01, 01);
            _dto.Name = "Test Period";
            _dto.NumberOfDays = 365;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }


        [TestMethod]
        public void TestConstructor()
        {
            Assert.IsNotNull(_dto);
        }

        [TestMethod]
        public void TestValidatePeriodName()
        {
            Assert.IsFalse(_dto.HasErrors);

            _dto.Name = "";
            
            Assert.IsTrue(_dto.HasErrors);
            var errors = _dto.GetErrors(nameof(_dto.Name)) as IEnumerable<string>;
            Assert.IsNotNull(errors);
            Assert.AreEqual("Name cannot be empty.", errors.ToList()[0]);
        }

        [TestMethod]
        public void TestValidateStartDate()
        {
            Assert.IsFalse(_dto.HasErrors);

            _dto.StartDate = new DateTime(2020, 01, 01);
            Assert.IsTrue(_dto.HasErrors);

            var errors = _dto.GetErrors(nameof(_dto.StartDate)) as IEnumerable<string>;
            Assert.IsNotNull(errors);
            Assert.AreEqual("Must be a valid date before the End Date.", errors.ToList()[0]);
        }

        [TestMethod]
        public void TestValidateEndDate()
        {
            Assert.IsFalse(_dto.HasErrors);

            _dto.EndDate = new DateTime(1998, 02, 08);
            Assert.IsTrue(_dto.HasErrors);

            var errors = _dto.GetErrors(nameof(_dto.EndDate)) as IEnumerable<string>;
            Assert.IsNotNull(errors);
            Assert.AreEqual("Must be a valid date later than the Start Date.", errors.ToList()[0]);
        }

        [TestMethod]
        public void TestValidateNumberOfDays()
        {
            Assert.IsFalse(_dto.HasErrors);

            _dto.NumberOfDays = -1;
            Assert.IsTrue(_dto.HasErrors);

            var errors = _dto.GetErrors(nameof(_dto.NumberOfDays)) as IEnumerable<string>;
            Assert.IsNotNull (errors);
            Assert.AreEqual("Must be greater than 0.", errors.ToList()[0]);
        }
    }
}