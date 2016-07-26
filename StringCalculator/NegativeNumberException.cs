using System;

namespace StringCalculator
{
    public class NegativeNumberException: Exception
    {
        private readonly int[] _negativeNumbers;
        public NegativeNumberException(string message) : base(message)
        {
        }

        public NegativeNumberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NegativeNumberException(int[] negativeNumbers)
        {
            _negativeNumbers = negativeNumbers;
        }

        public override string Message => $"negative not supported {string.Join(",", _negativeNumbers)}";
    }
}