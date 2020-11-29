using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Listener.Infrastructure
{
    public class Response
    {
        public string Content { get; }

        public HttpStatusCode Code { get; }

        public Response(string content, HttpStatusCode code)
        {
            Content = content;
            Code = code;
        }


        public static async Task<Response<TValue>> CreateValueResponse<TValue>(HttpResponseMessage response)
            where TValue : class
        {
            var content = await response.Content.ReadAsStringAsync();

            var value = DeserializeObject<TValue>(content);

            var error = DeserializeObject<ErrorModel>(content);

            return new Response<TValue>(value, content, response.StatusCode, error?.Error);
        }

        private static T DeserializeObject<T>(string content) where T : class
        {
            T result = null;

            try
            {
                result = JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings());
            }
            catch (JsonException)
            {
            }

            return result;
        }
    }
}
