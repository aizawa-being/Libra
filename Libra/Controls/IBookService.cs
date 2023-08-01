using System;
using System.Collections.Generic;

namespace Libra {
    /// <summary>
    /// 書籍DB操作用インターフェースです。
    /// </summary>
    public interface IBookService : IDisposable {

        /// <summary>
        /// 削除されていない書籍情報を全て取得します。
        /// </summary>
        /// <returns>books</returns>
        IEnumerable<Book> GetExistBooks();

        /// <summary>
        /// 書籍情報をDBに追加し、自動採番された書籍IDを通知します。
        /// </summary>
        /// <param name="vBook"></param>
        /// <returns>int</returns>
        int AddBook(Book vBook);
        
        /// 削除フラグを立てます
        /// </summary>
        /// <param name="vBookId"></param>
        void SetDeleteFlag(int vBookId);
    }
}
