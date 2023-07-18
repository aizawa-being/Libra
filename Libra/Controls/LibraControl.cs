using System.Collections.Generic;
using static Libra.BooksDataSet;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面のコントローラ
    /// </summary>
    public class LibraControl : ILibraControl {
        private readonly BooksTable FBooksTable;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl() {
            this.FBooksTable = new BooksTable();
        }

        /// <summary>
        /// 書籍一覧テーブルを初期化します。
        /// </summary>
        public void InitializeBookList() {
            using (var wBookService = new BookService()) {
                var wBooks = wBookService.GetExistBooks();
                this.SetBooksDataTable(wBooks);
            }
        }

        /// <summary>
        /// 書籍一覧テーブルを書籍一覧グリッドに設定します。
        /// </summary>
        /// <param name="vBooks"></param>
        /// <returns></returns>
        public void SetBooksDataTable(IEnumerable<Book> vBooks) {
            var wDataTable = new BooksDataTable();
            foreach (var wBook in vBooks) {
                wDataTable.Rows.Add(wBook.BookId,
                                   wBook.Title,
                                   wBook.Author,
                                   wBook.Publisher,
                                   wBook.Description,
                                   wBook.UserName);
            }
            this.FBooksTable.Books = wDataTable;
        }

        /// <summary>
        /// 書籍一覧テーブルの状態を取得します。
        /// </summary>
        /// <returns></returns>
        public BooksDataTable GetBooksDataTable() {
            return this.FBooksTable.Books;
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public void OpenAddForm() {
            IAddBookControl wAddBookControl = new AddBookControl();
            wAddBookControl.ShowAddBookForm();
        }
    }
}
