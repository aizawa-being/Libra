using Libra.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libra.Controls {
    /// <summary>
    /// OpenBDアクセス用インターフェースです。
    /// </summary>
    public interface IOpenBdConnect {
        Task<HttpResponseMessage> SendRequest(string vIsbn);
        Book PerseBookInfo(string vBookInfo);
    }
}
