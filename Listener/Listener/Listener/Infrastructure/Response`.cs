using System.Net;

namespace Listener.Infrastructure
{
    public class Response<T> : Response where T : class
    {
        public T Value { get; }

        public ErrorResponse Error { get; set; }

        public Response(T value, string content, HttpStatusCode code, ErrorResponse error = null)
            : base(content, code)
        {
            Value = value;
            Error = error;
        }
    }
}
