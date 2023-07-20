using System.Net.Http;
using System.Threading.Tasks;

namespace Libra {
    /// <summary>
    /// OpenBDアクセス用インターフェースです。
    /// </summary>
    public interface IOpenBdConnect {

        /// <summary>
        /// ISBNコードで書籍情報を非同期に取得します
        /// </summary>
        /// <param name="vIsbn"></param>
        /// <returns>Task<HttpResponseMessage></returns>
        Task<HttpResponseMessage> SendRequest(string vIsbn);

        /// <summary>
        /// 受け取ったJson形式の文字列から書籍情報を抽出します。
        /// </summary>
        /// <param name="vBookInfo"></param>
        /// <returns></returns>
        Book PerseBookInfo(string vBookInfo);
    }
}