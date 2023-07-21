using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libra {
    /// <summary>
    /// HttpCliantラップ用インターフェースです。
    /// </summary>
    public interface IHttpClient : IDisposable {

        /// <summary>
        /// レスポンスメッセージを受け取ります。
        /// </summary>
        /// <param name="vUrl"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string vUrl);
    }
}