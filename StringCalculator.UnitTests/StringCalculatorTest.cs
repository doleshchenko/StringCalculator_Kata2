using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace StringCalculator.UnitTests
{
    [TestClass]
    public class StringCalculatorTest
    {
        private const string LogMessageTemplate = "sum result: {0}";
        private Mock<ILogger> _loggerMock;
        private Mock<IWebservice> _webServiceMock;
        private Mock<IOutput> _outputMock;
        private StringCalculator _calculator;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger>();
            _webServiceMock = new Mock<IWebservice>();
            _outputMock = new Mock<IOutput>();
            _calculator = new StringCalculator(_loggerMock.Object, _webServiceMock.Object, _outputMock.Object);
        }

        [TestMethod]
        public void Add_EmptyStringAsInputParam_Returns0()
        {
            var result = _calculator.Add("");
            Assert.AreEqual(0, result, "Method should have returned a 0 for empty string.");
        }

        [TestMethod]
        public void Add_OneNumberAsInputParam_ReturnsInputParamAsNumber()
        {
            var result = _calculator.Add("1");
            Assert.AreEqual(1, result, "Method should have returned 1 for \"1\" string.");
        }

        [TestMethod]
        public void Add_TwoNumbersWithCommaDelimiterAsInputParam_ReturnsSumOfInputNumbers()
        {
            var result = _calculator.Add("1,2");
            Assert.AreEqual(3, result, "Method should have returned 3 for \"1,2\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedByCommaAsInputParam_ReturnsSumOfNumbers()
        {
            var result = _calculator.Add("1,2,3,5,9,5");
            Assert.AreEqual(25, result, "Method should have returned 25 for \"1,2,3,5,9,5\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedByCommaAndNewLineAsInputParam_ReturnsSumOfNumbers()
        {
            var result = _calculator.Add("1,2\n5");
            Assert.AreEqual(8, result, "Method should have returned 8 for \"1,2\n5\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedByCustomDelimiterAsInputParam_ReturnsSumOfNumbers()
        {
            var result = _calculator.Add("//;\n1;2;5");
            Assert.AreEqual(8, result, "Method should have returned 8 for \"//;\n1;2;5\" string.");
        }

        [TestMethod]
        public void Add_NegativeNumbersAsInputParam_ThrowsNegativeNumberException()
        {
            try
            {
                _calculator.Add("-1,2,3,-4");
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
            var result = _calculator.Add("1,1001,2");
            Assert.AreEqual(3, result, "Method should have returned 3 for \"1,1001,2\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedByCustomDelimiterOfAnyLengthAsInputParam_ReturnsSumOfNumbers()
        {
            var result = _calculator.Add("//*#*\n1*#*2*#*5");
            Assert.AreEqual(8, result, "Method should have returned 8 for \"//*#*\n1*#*2*#*5\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedBySeveralCustomDelimitersAsInputParam_ReturnsSumOfNumbers()
        {
            var result = _calculator.Add("//[*][%]\n1*2%3");
            Assert.AreEqual(6, result, "Method should have returned 6 for \"//[*][%]\n1*2%3\" string.");
        }

        [TestMethod]
        public void Add_SeveralNumbersDelimitedBySeveralCustomDelimitersOfDifferentLengthAsInputParam_ReturnsSumOfNumbers()
        {
            var result = _calculator.Add("//[*][%$%]\n1*2%$%3");
            Assert.AreEqual(6, result, "Method should have returned 6 for \"//[*][%$%]\n1*2%$%3\" string.");
        }

        [TestMethod]
        public void Add_TwoNumbersWithCommaDelimiterAsInputParam_ReturnsSumOfInputNumbersAndLogTheSum()
        {
            var result = _calculator.Add("1,2");
            const int expectedResult = 3;
            Assert.AreEqual(expectedResult, result, "Method should have returned 3 for \"1,2\" string.");
            _loggerMock.Verify(it => it.Write(string.Format(LogMessageTemplate, expectedResult)), Times.Once);
        }

        [TestMethod]
        public void Add_TwoNumbersWithCommaDelimiterAsInputParam_ReturnsSumOfInputNumbersAndLoggerThrowsException()
        {
            const int expectedResult = 3;

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(it => it.Write(string.Format(LogMessageTemplate, expectedResult))).Throws(new Exception(string.Format(LogMessageTemplate, expectedResult)));

            var webServiceMock = new Mock<IWebservice>();
            webServiceMock.Setup(it => it.LoggingFailed(string.Format(LogMessageTemplate, expectedResult)));

            var outputMock = new Mock<IOutput>();

            _calculator = new StringCalculator(loggerMock.Object, webServiceMock.Object, outputMock.Object);

            var result = _calculator.Add("1,2");
            Assert.AreEqual(expectedResult, result, "Method should have returned 3 for \"1,2\" string.");

            loggerMock.VerifyAll();
            webServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Add_TwoNumbersWithCommaDelimiterAsInputParam_WritesResultToOuput()
        {
            _calculator.Add("1,2");
            const int expectedResult = 3;
            _outputMock.Verify(it => it.WriteMessage($"The result is {expectedResult}"), Times.Once);
        }
    }
}
