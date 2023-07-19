using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// 書籍追加用コントローラーのインターフェースです。
    /// </summary>
    public interface IAddBookControl {
        Task SetAddBook(string vIsbn);
        Book GetAddBook();
        bool ExistAddBook();
        bool TryRegisterAddBook(Book vAddBook, out int vBookId);
        DialogResult MessageBoxShow(string vMessage, string vCaption, MessageBoxButtons vButton, MessageBoxIcon vIcon);
    }
}
