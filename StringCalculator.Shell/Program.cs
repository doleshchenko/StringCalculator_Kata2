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
                _output = new ConsoleOutput();
                _stringCalculator = new StringCalculator(null, null, _output);
                var calcParams = input.Substring(calcCommand.Length + 1).Replace("'", "");
                _stringCalculator.Add(calcParams);

            }
        }

        public static IOutput Ouput => _output;
    }
}
