using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        private readonly ILogger _logger;

        public StringCalculator(ILogger logger)
        {
            _logger = logger;
        }

        public int Add(string numbers)
        {
            var delimiters = new []{ ",", "\n"};
            var toCalculate = numbers;
            const string customDelimiterIndicator = "//";
            const int maxNumber = 1000;
            if (numbers.StartsWith(customDelimiterIndicator)) // has custom delimiter
            {
                var endOfCustomDelimiter = toCalculate.IndexOf("\n", StringComparison.InvariantCulture);
                var customDelimiter = numbers.Substring(customDelimiterIndicator.Length, endOfCustomDelimiter - customDelimiterIndicator.Length);

                var regExp = new Regex(@"\[(.*?)\]");
                delimiters = regExp.IsMatch(customDelimiter)
                    ? regExp.Split(customDelimiter).Where(it => !string.IsNullOrEmpty(it)).ToArray()
                    : new[] {customDelimiter};

                toCalculate = toCalculate.Substring(endOfCustomDelimiter + 1);
            }
            var realNumbers = toCalculate.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var negatives = realNumbers.Where(it => it < 0).ToArray();
            if (negatives.Any())
            {
                throw new NegativeNumberException(negatives.ToArray());
            }
            var numbersToCalculate = realNumbers.Where(it => it <= maxNumber).ToArray();
            var sumResult = numbersToCalculate.Sum();

            _logger.Write($"sum result: {sumResult}");

            return sumResult;
        }
    }
}
