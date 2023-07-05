using Libra.Models;
using System.Collections.Generic;
using static Libra.Models.BooksDataSet;

namespace Libra.Controls {
    public class LibraController {

        private BooksTable F_BooksTable;

        public LibraController() {
            this.F_BooksTable = new BooksTable();
        }

        /// <summary>
        /// 書籍一覧テーブルを初期化します。
        /// </summary>
        public void InitializeBookList() {
            var bookService = new BookService();
            var books = bookService.GetExistBooks();
            this.SetBooksDataTable(books);
        }
        
        /// <summary>
        /// 現在の書籍一覧テーブルを取得します。
        /// </summary>
        /// <returns>BooksDataTable</returns>
        public BooksDataTable GetBooksDateTable() {
            return this.F_BooksTable.Books;
        }

        /// <summary>
        /// 書籍一覧テーブルを書籍一覧グリッドに設定します。
        /// </summary>
        /// <param name="vBooks"></param>
        /// <returns></returns>
        public void SetBooksDataTable(IEnumerable<Book> vBooks) {
            var dataTable = new BooksDataTable();
            foreach (var book in vBooks) {
                dataTable.Rows.Add(book.BookId,
                                   book.Title,
                                   book.Author,
                                   book.Publisher,
                                   book.Description,
                                   book.UserName);
            }
            this.F_BooksTable.Books = dataTable;
        }
    }
}
