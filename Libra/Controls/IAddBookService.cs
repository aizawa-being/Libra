using System;

namespace Libra {
    /// <summary>
    /// 追加画面での書籍DB操作インターフェースです。
    /// </summary>
    public interface IAddBookService : IDisposable {

        /// <summary>
        /// 書籍情報をDBに追加し、自動採番された書籍IDを通知します。
        /// </summary>
        /// <param name="vBook"></param>
        /// <returns>int</returns>
        int AddBook(Book vBook);
    }
}
