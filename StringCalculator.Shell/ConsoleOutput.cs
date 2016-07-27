using System;

namespace StringCalculator.Shell
{
    public class ConsoleOutput: IOutput
    {
        private string _text;
        public void Write(string text)
        {
            _text = text;
            Console.WriteLine(text);
        }

        public string GetLastMessage()
        {
            return _text;
        }
    }
}