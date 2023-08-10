using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;

using static Libra.BooksDataSet;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面のコントローラクラスです。
    /// </summary>
    public class LibraControl : ILibraControl {
        private readonly BooksTable FBooksTable;
        private readonly Func<IBookRepository> FBookRepository;
        private readonly IMessageBoxService FMessageBoxService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl() {
            this.FBooksTable = new BooksTable();
            this.FBookRepository = () => new BookRepository(new BooksDbContext());
            this.FMessageBoxService = new MessageBoxService();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl(BooksTable vBooksTable, Func<IBookRepository> vFunc, IMessageBoxService vMessageBoxService) {
            this.FBooksTable = vBooksTable;
            this.FBookRepository = vFunc;
            this.FMessageBoxService = vMessageBoxService;
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public int OpenAddForm() {
            IAddBookControl wAddBookControl = new AddBookControl();
            return wAddBookControl.ShowAddBookForm();
        }

        /// <summary>
        /// 書籍一覧テーブルを初期化します。
        /// </summary>
        public void InitializeBookList() {
            using (ILibraBookService wBookService = new BookService(this.FBookRepository)) {
                try {
                    var wBooks = wBookService.GetExistBooks();
                    this.SetBooksDataTable(wBooks);

                } catch (Exception vException) {
                    this.HandleException(vException);
                }
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
        /// 削除フラグを設定します。
        /// </summary>
        public bool SetDeleteFlag(string vTitle, int vBookId) {
            // 削除確認メッセージボックスの表示
            if (this.FMessageBoxService.Show(MessageTypeEnum.DeleteConfirmation, vTitle) != DialogResult.OK) {
                return false;
            }

            try {
                using (ILibraBookService wBookService = new BookService(this.FBookRepository)) {
                    wBookService.SetDeleteFlag(vBookId);
                    return true;
                }

            } catch (Exception vException) {
                this.HandleException(vException);
            }
            return false;
        }

        /// <summary>
        /// 書籍を貸出中にします。
        /// </summary>
        /// <param name="vUserName"></param>
        /// <param name="vBookId"></param>
        /// <returns></returns>
        public void BorrowBook(string vUserName, int vBookId) {
            // 利用者名は入力が必須
            if (string.IsNullOrWhiteSpace(vUserName)) {
                this.FMessageBoxService.Show(MessageTypeEnum.UserNameNotInput);
                return;
            }
            try {
                using (ILibraBookService wBookService = new BookService(this.FBookRepository)) {
                    wBookService.BorrowBook(vBookId, vUserName);
                }

            } catch (Exception vException) {
                this.HandleException(vException);
            }
        }

        /// <summary>
        /// 書籍を返却します。
        /// </summary>
        /// <param name="vBookId"></param>
        public void ReturnBook(int vBookId) {
            try {
                using (ILibraBookService wBookService = new BookService(this.FBookRepository)) {
                    wBookService.ReturnBook(vBookId);
                }
            } catch (BookOperationException vException) {
                var wBookError = new BookErrorDefine(vException.ErrorType);
                this.FMessageBoxService.Show(wBookError.FMessageType, vException.BookTitle);

            } catch (Exception vException) {
                this.HandleException(vException);
            }
        }

        /// <summary>
        /// 例外を処理します。
        /// </summary>
        /// <param name="vException"></param>
        private void HandleException(Exception vException) {
            if (vException is BookOperationException wBookOperationException) {
                // Libra独自の例外発生
                var wBookError = new BookErrorDefine(wBookOperationException.ErrorType);
                this.FMessageBoxService.Show(wBookError.FMessageType, wBookOperationException.BookTitle);

            } else if (vException is DbException || vException is DbUpdateException || vException is EntityException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } else {
                // 予期せぬエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.UnexpectedError, vException);
            }
        }
    }
}
