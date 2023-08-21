using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Func<IBookRepository> FBookRepository;
        private readonly IMessageBoxUtil FMessageBoxService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl() {
            this.FBookRepository = () => new BookRepository(new BooksDbContext());
            this.FMessageBoxService = new MessageBoxUtil();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl(Func<IBookRepository> vFunc, IMessageBoxUtil vMessageBoxService) {
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
        /// 書籍一覧を取得します。
        /// </summary>
        public IEnumerable<Book> GetAllBooks() {
            using (ILibraBookService wBookService = new BookService(this.FBookRepository)) {
                try {
                    return wBookService.GetExistBooks();

                } catch (Exception vException) {
                    this.HandleException(vException);
                }
                return null;
            }
        }

        /// <summary>
        /// 書籍一覧をBooksDataTableに変換します。
        /// </summary>
        /// <param name="vBooks"></param>
        /// <returns></returns>
        public BooksDataTable ConvertBooksDataTable(IEnumerable<Book> vBooks) {
            var wDataTable = new BooksDataTable();
            foreach (var wBook in vBooks) {
                wDataTable.Rows.Add(wBook.BookId,
                                    wBook.Title,
                                    wBook.Author,
                                    wBook.Publisher,
                                    wBook.Description,
                                    wBook.UserName);
            }
            return wDataTable;
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

        /// <summary>
        /// 書籍を検索します。
        /// </summary>
        /// <param name="vSearchWord"></param>
        public IEnumerable<Book> SearchBooks(string vSearchString) {
            // 検索ワードを半角空白で分割する。
            IEnumerable<string> wSearchWords = vSearchString.Split(' ').Select(s => s.Trim());

            try {
                using (ILibraBookService wBookService = new BookService(this.FBookRepository)) {
                    return wBookService.SearchBooks(wSearchWords);
                }
            } catch (DbException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (EntityException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (Exception vException) {
                // 予期せぬエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.UnexpectedError, vException);
            }
            return null;
        }
    }
}
