namespace Application.Common.Exceptions
{
    public class MultiplyMessageException : Exception
    {
        public List<string> Messages { get; set; }
        public MultiplyMessageException(List<string> messages)
        {
            Messages = messages;
        }
    }
}
