using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Listener
{
    public class FileContent : MultipartFormDataContent
    {
        public FileContent(string fileName, string contentType, Stream stream)
        {
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            Add(streamContent, "file", fileName);
        }
    }
}
