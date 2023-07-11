using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libra.Controls {
    /// <summary>
    /// HttpCliantラップ用インターフェースです。
    /// </summary>
    public interface IHttpClient : IDisposable {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}