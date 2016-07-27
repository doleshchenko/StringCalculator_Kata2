namespace StringCalculator
{
    public interface IOutput
    {
        void Write(string text);
        string GetLastMessage();
    }
}