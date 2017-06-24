namespace RulesEngine.Model
{
    public class ErrorMessage
    {
        public readonly Error Error;
        public readonly string Message;

        public ErrorMessage(Error error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}