using System;

namespace StringCalculator.Shell
{
    public class Program
    {
        private static StringCalculator _stringCalculator;
        private static IOutput _output;

        public static void Main(string[] args)
        {
            var input = args[0];
            var calcCommand = "scalc";
            if (input.IndexOf(calcCommand, StringComparison.InvariantCulture) == 0)
            {
                _stringCalculator = new StringCalculator(null, null, Ouput);
                var calcParams = input.Substring(calcCommand.Length + 1).Replace("'", "");
                do
                {
                    _stringCalculator.Add(calcParams);
                    Ouput.AskQuestion("another input please: ");
                    calcParams = Ouput.GetAnswer();
                } while (!string.IsNullOrEmpty(calcParams));
            }
        }

        public static IOutput Ouput
        {
            get { return _output ?? (_output = new ConsoleOutput()); }
            set { _output = value; }
        }
    }
}
