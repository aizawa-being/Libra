using System.Net.Http;
using System.Threading.Tasks;

namespace Libra.Controls {
    /// <summary>
    /// HttpClientのラッパークラスです
    /// </summary>
    public class HttpClientWrapper : IHttpClient {
        private readonly HttpClient F_HttpClient;
        
        public HttpClientWrapper() {
            this.F_HttpClient = new HttpClient();
        }

        /// <summary>
        /// 指定された URI に GET 要求を非同期操作として送信します。
        /// </summary>
        /// <param name="vUrl"></param>
        /// <returns>HttpResponseMessage</returns>
        public Task<HttpResponseMessage> GetAsync(string vUrl) {
            return this.F_HttpClient.GetAsync(vUrl);
        }

        /// <summary>
        /// リリースで使用されるアンマネージ リソースおよびマネージ リソースを破棄
        /// </summary>
        public void Dispose() {
            this.F_HttpClient.Dispose();
        }
    }
}
