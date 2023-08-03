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
        void InitializeBookList();

        /// <summary>
        /// 書籍一覧テーブルを書籍一覧グリッドに設定します。
        /// </summary>
        /// <param name="vBooks"></param>
        /// <returns></returns>
        void SetBooksDataTable(IEnumerable<Book> vBooks);

        /// <summary>
        /// 書籍一覧テーブルの状態を取得します。
        /// </summary>
        /// <returns></returns>
        BooksDataTable GetBooksDataTable();

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        int OpenAddForm();

        /// <summary>
        /// 削除フラグを立てます。
        /// </summary>
        bool SetDeleteFlag(string vTitle, int vBookId);
    }
}
