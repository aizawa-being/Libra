using System;
using System.Collections.Generic;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面での書籍DB操作用インターフェースです。
    /// </summary>
    public interface ILibraBookService : IDisposable {

        /// <summary>
        /// 削除されていない書籍情報を全て取得します。
        /// </summary>
        /// <returns>books</returns>
        IEnumerable<Book> GetExistBooks();
        
        /// 削除フラグを立てます
        /// </summary>
        /// <param name="vBookId"></param>
        void SetDeleteFlag(int vBookId);
        
        /// <summary>
        /// 書籍を複数ワードで検索します。
        /// </summary>
        /// <param name="vSearchWords"></param>
        /// <returns>Book</returns>
        IEnumerable<Book> SearchBooks(IEnumerable<string> vSearchWords);
    }
}
