using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace TestIT.Listener
{
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, MediaTypeNames.Application.Json) { }
    }
}
