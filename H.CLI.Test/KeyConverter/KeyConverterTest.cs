﻿namespace H.CLI.Test.KeyConverter
{
    [TestClass]
    public class KeyConverterTest
    {

        [TestMethod]
        public void Test_ConvertTemplateKey_Expect_SpacesBetweenAllCapitalLetters()
        {
            var keyConverter = new CLI.KeyConverter.KeyConverter();
            var result1 = keyConverter.ConvertTemplateKey("RowName");
            var result2 = keyConverter.ConvertTemplateKey("PercentMortalityCategory(0/15/30/50)");
            Assert.AreEqual(result1, "Row Name");
            Assert.AreEqual(result2, "Percent Mortality Category(0/15/30/50)");
        }

        [TestMethod]
        public void Test_ConvertResponseToBool_Expect_YesToReturnTrueNoToReturnFalse()
        {
            var keyConverter = new CLI.KeyConverter.KeyConverter();
            var result1 = keyConverter.ConvertResponseToBool("Yes");
            var result2 = keyConverter.ConvertResponseToBool("No");
            Assert.AreEqual(result1, true);
            Assert.AreEqual(result2, false);
        }
    }
}

