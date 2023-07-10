using Libra.Models;
using System.Threading.Tasks;

namespace Libra.Controls {
    public interface IOpenBdConnect {
        Task<string> GetBookByIsbn(string vIsbn);
        Book PerseBookInfo(string vBookInfo);
    }
}
