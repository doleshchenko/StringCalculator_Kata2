using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.UnitTests.Stubs
{
    public class OutputStub : IOutput
    {
        private readonly List<string> _messages = new List<string>();
        private readonly List<string> _questions = new List<string>(); 
        private readonly List<string> _answers = new List<string>();
        private int _answerCounter = 0;

        public List<string> Messages => _messages;
        public List<string> Questions => _questions;
        public List<string> Answers => _answers;

        public OutputStub(string[] answers)
        {
            _answers.AddRange(answers);
        }

        public void WriteMessage(string message)
        {
            _messages.Add(message);
        }

        public void AskQuestion(string question)
        {
            _questions.Add(question);
        }

        public string GetLastMessage()
        {
            return _messages.Last();
        }

        public string GetLastQuestion()
        {
            return _questions.Last();
        }

        public string GetAnswer()
        {
            return _answerCounter >= _answers.Count ? string.Empty : _answers[_answerCounter++];
        }
    }
}