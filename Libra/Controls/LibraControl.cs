using System;
using System.Collections.Generic;

using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Windows.Forms;
using static Libra.BooksDataSet;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面のコントローラ
    /// </summary>
    public class LibraControl : ILibraControl {
        private readonly BooksTable FBooksTable;
        private readonly IBookRepository FBookRepository;
        private readonly IMessageBoxService FMessageBoxService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl() {
            this.FBooksTable = new BooksTable();
            this.FBookRepository = new BookRepository(new BooksDbContext());
            this.FMessageBoxService = new MessageBoxService();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraControl(BooksTable vBooksTable, IBookRepository vBookRepository, IMessageBoxService vMessageBoxService) {
            this.FBooksTable = vBooksTable;
            this.FBookRepository = vBookRepository;
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
        public bool SetDeleteFlag(int vBookId) {
            var wResult = false;
            try {
                using (var wBooksService = new BookService(this.FBookRepository)) {
                    wBooksService.SetDeleteFlag(vBookId);
                    wResult = true;
                }
            } catch (BookOperationException vException) {
                var wBookError = new BookErrorDefine(vException.ErrorType);
                this.FMessageBoxService.Show(string.Format(wBookError.ErrorMessage, vException.BookTitle), wBookError.ErrorCaption, wBookError.BoxButton, wBookError.BoxIcon);
            } catch (DbException) {
                // DBエラー発生
                this.FMessageBoxService.Show(ErrorMessageConst.C_DbError,
                                             ErrorMessageConst.C_DbErrorCaption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);

            } catch (DbUpdateException) {
                // DBエラー発生
                this.FMessageBoxService.Show(ErrorMessageConst.C_DbError,
                                             ErrorMessageConst.C_DbErrorCaption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);

            } catch (Exception vException) {
                // 予期せぬエラー発生
                this.FMessageBoxService.Show(string.Format(ErrorMessageConst.C_UnexpectedError, vException),
                                             ErrorMessageConst.C_UnexpectedErrorCaption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
            return wResult;
        }
    }
}
