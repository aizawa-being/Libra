using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;

using static Libra.BooksDataSet;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面のコントローラ
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
        /// 削除フラグを立てます。
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
            } catch (BookOperationException vException) {
                var wBookError = new BookErrorDefine(vException.ErrorType);
                this.FMessageBoxService.Show(wBookError.FMessageType, vException.BookTitle);

            } catch (DbException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (DbUpdateException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (EntityException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (Exception vException) {
                // 予期せぬエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.UnexpectedError, vException);
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
            if (string.IsNullOrEmpty(vUserName)) {
                this.FMessageBoxService.Show(MessageTypeEnum.UserNameNotInput);
                return;
            }
            try {
                using (ILibraBookService wBookService = new BookService(this.FBookRepository)) {
                    wBookService.BorrowBook(vBookId, vUserName);
                }
            } catch (BookOperationException vException) {
                var wBookError = new BookErrorDefine(vException.ErrorType);
                this.FMessageBoxService.Show(wBookError.FMessageType, vException.BookTitle);

            } catch (DbException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (DbUpdateException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (EntityException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (Exception vException) {
                // 予期せぬエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.UnexpectedError, vException);
            }
        }

        public void ReturnBook(int vBookId) {
            try {
                using (ILibraBookService wBookService = new BookService(this.FBookRepository)) {
                    wBookService.ReturnBook(vBookId);
                }
            } catch (BookOperationException vException) {
                var wBookError = new BookErrorDefine(vException.ErrorType);
                this.FMessageBoxService.Show(wBookError.FMessageType, vException.BookTitle);

            } catch (DbException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (DbUpdateException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (EntityException) {
                // DBエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.DbError);

            } catch (Exception vException) {
                // 予期せぬエラー発生
                this.FMessageBoxService.Show(MessageTypeEnum.UnexpectedError, vException);
            }
        }
    }
}
