namespace StudentApi.Exceptions
{
    public class ItemDoesNotExist : Exception
    {
            public ItemDoesNotExist(string? message):base(message) { }
    }
}
