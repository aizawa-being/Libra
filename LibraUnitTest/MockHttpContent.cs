using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraUnitTest {
    public class MockHttpContent : HttpContent {
        private readonly string F_content;

        public MockHttpContent() {
            this.F_content = "";
        }

        public MockHttpContent(string vContent) {
            this.F_content = vContent;
        }

        protected override Task<Stream> CreateContentReadStreamAsync() {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(this.F_content));
            return Task.FromResult<Stream>(stream);
        }

        protected override async Task SerializeToStreamAsync(Stream vStream, TransportContext vContext) {
            var contentBytes = Encoding.UTF8.GetBytes(this.F_content);
            await vStream.WriteAsync(contentBytes, 0, contentBytes.Length);
        }

        protected override bool TryComputeLength(out long vLength) {
            vLength = Encoding.UTF8.GetByteCount(this.F_content);
            return true;
        }
    }
}
