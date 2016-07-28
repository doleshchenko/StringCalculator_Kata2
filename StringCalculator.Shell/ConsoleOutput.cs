using System;

namespace StringCalculator.Shell
{
    public class ConsoleOutput: IOutput
    {
        private string _message;
        private string _question;
        private string _answer;

        public void WriteMessage(string message)
        {
            _message = message;
            Console.WriteLine(message);
        }

        public void AskQuestion(string question)
        {
            _question = question;
            Console.WriteLine(_question);
            _answer = Console.ReadLine();
        }

        public string GetLastMessage()
        {
            return _message;
        }

        public string GetLastQuestion()
        {
            return _question;
        }

        public string GetAnswer()
        {
            return _answer;
        }
    }
}