namespace StudentApi.Exceptions
{
    public class InvalidAge : Exception
    {
        public InvalidAge(string? message) : base(message) { }
    }
}
