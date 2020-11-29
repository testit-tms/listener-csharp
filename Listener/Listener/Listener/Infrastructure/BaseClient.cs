using System.Net.Http;

namespace Listener.Infrastructure
{
    public abstract class BaseClient
    {
        protected readonly HttpClient client;

        protected BaseClient(HttpClient client)
        {
            this.client = client;
        }
    }
}
