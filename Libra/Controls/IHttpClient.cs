using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libra {
    /// <summary>
    /// HttpCliantラップ用インターフェースです。
    /// </summary>
    public interface IHttpClient : IDisposable {
        Task<HttpResponseMessage> GetAsync(string vUrl);
    }
}