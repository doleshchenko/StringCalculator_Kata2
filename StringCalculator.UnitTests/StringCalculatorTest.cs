using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringCalculator.UnitTests
{
    [TestClass]
    public class StringCalculatorTest
    {
        [TestMethod]
        public void Add_EmptyStringAsInputParam_Returns0()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("");
            Assert.AreEqual(0, result, "Method should have returned a 0 for empty string.");
        }

        [TestMethod]
        public void Add_OneNumberAsInputParam_ReturnsInputParamAsNumber()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1");
            Assert.AreEqual(1, result, "Method should have returned 1 for \"1\" string.");
        }

        [TestMethod]
        public void Add_TwoNumbersWithCommaDelimiterAsInputParam_ReturnsSumOfInputNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1,2");
            Assert.AreEqual(3, result, "Method should have returned 3 for \"1,2\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedByCommaAsInputParam_ReturnsSumOfNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1,2,3,5,9,5");
            Assert.AreEqual(25, result, "Method should have returned 25 for \"1,2,3,5,9,5\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedByCommaAndNewLineAsInputParam_ReturnsSumOfNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1,2\n5");
            Assert.AreEqual(8, result, "Method should have returned 8 for \"1,2\n5\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedByCustomDelimiterAsInputParam_ReturnsSumOfNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("//;\n1;2;5");
            Assert.AreEqual(8, result, "Method should have returned 8 for \"//;\n1;2;5\" string.");
        }

        [TestMethod]
        public void Add_NegativeNumbersAsInputParam_ThrowsNegativeNumberException()
        {
            var calculator = new StringCalculator();

            try
            {
                calculator.Add("-1,2,3,-4");
            }
            catch (NegativeNumberException e)
            {
                var message = e.Message;
                Assert.IsTrue(message.Contains("-1"));
                Assert.IsTrue(message.Contains("-4"));
                Assert.IsTrue(message.Contains("negative not supported"));
                return;
            }
            Assert.Fail("Method should have thrown a NegativeNumberException which text contains -1 and -4");
        }

        [TestMethod]
        public void Add_SeveralNumbersWhereOneNumberIsMoreThan1000AsInputParam_IgnoreNumberBiggerThan1000ReturnsSumOfNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1,1001,2");
            Assert.AreEqual(3, result, "Method should have returned 3 for \"1,1001,2\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedByCustomDelimiterOfAnyLengthAsInputParam_ReturnsSumOfNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("//*#*\n1*#*2*#*5");
            Assert.AreEqual(8, result, "Method should have returned 8 for \"//*#*\n1*#*2*#*5\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedBySeveralCustomDelimitersAsInputParam_ReturnsSumOfNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("//[*][%]\n1*2%3");
            Assert.AreEqual(6, result, "Method should have returned 6 for \"//[*][%]\n1*2%3\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedBySeveralCustomDelimitersOfDifferentLengthAsInputParam_ReturnsSumOfNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("//[*][%$%]\n1*2%$%3");
            Assert.AreEqual(6, result, "Method should have returned 6 for \"//[*][%$%]\n1*2%$%3\" string.");
        }
    }
}
