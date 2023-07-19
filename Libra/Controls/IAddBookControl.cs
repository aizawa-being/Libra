using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// 書籍追加用コントローラーのインターフェースです。
    /// </summary>
    public interface IAddBookControl {

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        int ShowAddBookForm();

        /// <summary>
        /// ISBNコードと一致する書籍情報を設定します。
        /// </summary>
        /// <param name="vIsbn"></param>
        /// <returns>true  : 成功
        ///          false : 失敗</returns>
        Task SetAddBook(string vIsbn);

        /// <summary>
        /// 追加用書籍情報の取得
        /// </summary>
        /// <returns></returns>
        Book GetAddBook();

        /// <summary>
        /// 追加用書籍情報が存在するか
        /// </summary>
        /// <returns>true : 存在する
        ///         false : null</returns>
        bool ExistAddBook();

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <param name="vButton"></param>
        /// <param name="vIcon"></param>
        DialogResult MessageBoxShow(string vMessage, string vCaption, MessageBoxButtons vButton, MessageBoxIcon vIcon);
    }
}