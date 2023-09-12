using System.Net.Http;
using System.Threading.Tasks;

namespace Libra {
    /// <summary>
    /// HttpClientのラッパークラスです
    /// </summary>
    public class HttpClientWrapper : IHttpClient {
        private readonly HttpClient FHttpClient;
        
        public HttpClientWrapper() {
            this.FHttpClient = new HttpClient();
        }

        /// <summary>
        /// 指定されたURlにGET要求を非同期操作として送信します。
        /// </summary>
        /// <param name="vUrl"></param>
        /// <returns>HttpResponseMessage</returns>
        public Task<HttpResponseMessage> GetAsync(string vUrl) {
            return this.FHttpClient.GetAsync(vUrl);
        }

        /// <summary>
        /// リリースで使用されるアンマネージリソースおよびマネージリソースを破棄
        /// </summary>
        public void Dispose() {
            this.FHttpClient.Dispose();
        }
    }
}