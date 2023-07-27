using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace servico_certificado.Tests
{
    public class TestRequestBody
    {
        private readonly object _data;

        public TestRequestBody(object data)
        {
            _data = data;
        }

        public Stream ToStream()
        {
            var json = JsonConvert.SerializeObject(_data);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return stream;
        }
    }
}
