namespace Listener.Infrastructure
{
    public class ErrorModel
    {
        public ErrorResponse Error { get; set; }
    }

    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public string[] Stack { get; set; }
    }
}
