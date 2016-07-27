using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator.Shell;

namespace StringCalculator.UnitTests
{
    [TestClass]
    public class StringCalculatorShellTest
    {
        [TestMethod]
        public void Main_InvokeStringCalculator_OutputCalculationResultToConsole()
        {
            Program.Main(new[] { "scalc'1,2,3'" });
            var outputString = Program.Ouput.GetLastMessage();
            Assert.AreEqual("The result is 6", outputString, "Wrong console message.");
        }
    }
}