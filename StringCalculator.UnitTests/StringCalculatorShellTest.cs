using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator.Shell;
using StringCalculator.UnitTests.Stubs;

namespace StringCalculator.UnitTests
{
    [TestClass]
    public class StringCalculatorShellTest
    {
        [TestMethod]
        public void Main_InvokeStringCalculator_OutputCalculationResultToConsole()
        {
            Program.Main(new[] { "scalc'1,2,3'" });
            var message = Program.Ouput.GetLastMessage();
            Assert.AreEqual("The result is 6", message, "Wrong console message.");
        }

        [TestMethod]
        public void Main_InvokeStringCalculator_OutputCalculationResultToConsole1()
        {
            var outputStub = new OutputStub(new []{ "4,5,6" });
            Program.Ouput = outputStub;
            Program.Main(new[] { "scalc'1,2,3'" });
            var messages = outputStub.Messages;
            var questions = outputStub.Questions;

            Assert.AreEqual(2, messages.Count, "Should be generated 2 messages.");
            var message1 = messages[0];
            Assert.AreEqual("The result is 6", message1, "Wrong console message was generated.");
            var message2 = messages[1];
            Assert.AreEqual("The result is 15", message2, "Wrong console message was generated.");

            Assert.AreEqual(2, questions.Count, "Should be generated 2 questions.");
            var question1 = questions[0];
            var question2 = questions[1];
            Assert.AreEqual(question1, question2, "The questions should be the same.");
            Assert.AreEqual("another input please: ", question1, "Wrong question was generated.");
        }
    }
}