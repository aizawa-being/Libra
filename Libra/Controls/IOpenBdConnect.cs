using Libra.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libra.Controls {
    public interface IOpenBdConnect {
        Task<HttpResponseMessage> SendRequest(string vIsbn);
        Book PerseBookInfo(string vBookInfo);
    }
}
