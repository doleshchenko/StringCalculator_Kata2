namespace StringCalculator
{
    public interface IOutput
    {
        void WriteMessage(string message);
        void AskQuestion(string question);
        string GetLastMessage();
        string GetLastQuestion();
        string GetAnswer();
    }
}