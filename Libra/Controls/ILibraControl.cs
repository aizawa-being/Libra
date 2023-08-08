using System.Collections.Generic;
using static Libra.BooksDataSet;

﻿namespace Libra {
    /// <summary>
    /// 書籍一覧画面のコントローラ用インターフェース
    /// </summary>
    public interface ILibraControl {

        /// <summary>
        /// 書籍一覧テーブルを初期化します。
        /// </summary>
        IEnumerable<Book> SetAllBooks();

        /// <summary>
        /// 書籍一覧をBooksDataTableに変換します。
        /// </summary>
        /// <param name="vBooks"></param>
        /// <returns></returns>
        BooksDataTable ConvertBooksDataTable(IEnumerable<Book> vBooks);

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        int OpenAddForm();

        /// <summary>
        /// 削除フラグを立てます。
        /// </summary>
        bool SetDeleteFlag(string vTitle, int vBookId);

        /// <summary>
        /// 書籍を検索します。
        /// </summary>
        /// <param name="vSearchWord"></param>
        IEnumerable<Book> SearchBooks(string vSearchString);
    }
}
