using System.Threading.Tasks;

namespace Libra {
    /// <summary>
    /// 書籍追加用コントローラーのインターフェースです。
    /// </summary>
    public interface IAddBookController {
        Task SetAddBook(string vIsbn);
        Book GetAddBook();
        bool ExistAddBook();
        int RegisterAddBook();
    }
}
