using System;
using System.Collections.Generic;

using System.Data.Common;

using static Libra.BooksDataSet;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面のコントローラ
    /// </summary>
    public class LibraControl : ILibraControl {
        private readonly BooksTable FBooksTable;
        private readonly IBookRepository FBookRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl() {
            this.FBooksTable = new BooksTable();
            this.FBookRepository = new BookRepository(new BooksDbContext());
        }

        /// <summary>
        /// 書籍一覧テーブルを初期化します。
        /// </summary>
        public void InitializeBookList() {
            using (var wBookService = new BookService(this.FBookRepository)) {
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
        public int OpenAddForm() {
            IAddBookControl wAddBookControl = new AddBookControl();
            return wAddBookControl.ShowAddBookForm();
        }

        /// <summary>
        /// 削除フラグを立てます。
        /// </summary>
        public void SetDeleteFlag(int vBookId) {
            try {
                using (var wBooksService = new BookService(this.FBookRepository)) {
                    wBooksService.SetDeleteFlag(vBookId);
                }
            } catch (InvalidOperationException e) {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine(e);
                Console.WriteLine("---------------------------------------------------------");

            } catch (DbException e) {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine(e);
                Console.WriteLine("---------------------------------------------------------");

            } catch (Exception e) {
                // 予期せぬエラー
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine(e);
                Console.WriteLine("---------------------------------------------------------");
            }
        }
    }
}
