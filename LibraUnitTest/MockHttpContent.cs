using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraUnitTest {
    public class MockHttpContent : HttpContent {
        private readonly string FContent;
        
        public MockHttpContent(string vContent) {
            this.FContent = vContent;
        }

        protected override Task<Stream> CreateContentReadStreamAsync() {
            var wStream = new MemoryStream(Encoding.UTF8.GetBytes(this.FContent));
            return Task.FromResult<Stream>(wStream);
        }

        protected override async Task SerializeToStreamAsync(Stream vStream, TransportContext vContext) {
            var wContentBytes = Encoding.UTF8.GetBytes(this.FContent);
            await vStream.WriteAsync(wContentBytes, 0, wContentBytes.Length);
        }

        protected override bool TryComputeLength(out long vLength) {
            vLength = Encoding.UTF8.GetByteCount(this.FContent);
            return true;
        }
    }
}