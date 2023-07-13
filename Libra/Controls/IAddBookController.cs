using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// 書籍追加用コントローラーのインターフェースです。
    /// </summary>
    public interface IAddBookController {
        Task SetAddBook(string vIsbn);
        Book GetAddBook();
        bool ExistAddBook();
        int RegisterAddBook(Book vBook);
        DialogResult MessageBoxShow(string vMessage, string vCaption, MessageBoxButtons vButton, MessageBoxIcon vIcon);
    }
}
