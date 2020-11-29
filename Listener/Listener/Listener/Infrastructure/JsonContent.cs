using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Listener.Infrastructure
{
    public class JsonContent : StringContent
    {
        private static readonly DefaultContractResolver defaultContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = defaultContractResolver,
            Formatting = Formatting.Indented
        };

        public JsonContent(object obj) : base(JsonConvert.SerializeObject(obj, jsonSerializerSettings),
            Encoding.UTF8, MediaTypeNames.Application.Json)
        {
        }
    }
}
