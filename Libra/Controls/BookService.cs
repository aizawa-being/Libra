using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.SQLite;

namespace Libra {

    /// <summary>
    /// 書籍関連のサービスを提供します。
    /// 書籍関連の個別の処理はサービスで実装してください。
    /// </summary>
    public class BookService : ILibraBookService, IAddBookService {
        private readonly Func<IBookRepository> FBookRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BookService(Func<IBookRepository> vFunc) {
            this.FBookRepository = vFunc;
        }

        /// <summary>
        /// トランザクション内で指定されたアクションを実行します。
        /// トランザクションの開始、コミット、および例外時のロールバックを管理します。
        /// </summary>
        /// <param name="vAction"></param>
        private void PerformInTransaction(Action<IBookRepository> vAction) {
            IBookRepository wBookRepository = this.FBookRepository();
            // トランザクション開始
            wBookRepository.BeginTransaction();
            try {
                // 指定されたアクションを実行
                vAction(wBookRepository);
                wBookRepository.Save();
                wBookRepository.CommitTransaction();

            } catch (Exception) {
                // ロールバック
                wBookRepository.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// 削除されていない書籍情報を全て取得します。
        /// </summary>
        /// <returns>books</returns>
        public IEnumerable<Book> GetExistBooks() {
            IEnumerable<Book> wBooks = new List<Book>();

            this.PerformInTransaction(wRepository => {
                wBooks = from book in wRepository.GetBooks()
                         where book.IsDeleted is 0
                         orderby book.Title
                         select book;
            });
            return wBooks;
        }

        /// <summary>
        /// 書籍情報をDBに追加し、自動採番された書籍IDを通知します。
        /// </summary>
        /// <param name="vBook"></param>
        /// <returns>int</returns>
        public int AddBook(Book vBook) {
            this.PerformInTransaction(wRepository => {
                wRepository.AddBook(vBook);
            });
            return vBook.BookId;
        }

        /// <summary>
        /// 削除フラグを設定します。
        /// </summary>
        /// <param name="vBookId"></param>
        public void SetDeleteFlag(int vBookId) {
            this.PerformInTransaction(wRepository => {
                var wBook = wRepository.GetBookById(vBookId);
                if (wBook == null) {
                    throw new SQLiteException();
                }
                if (wBook.IsDeleted == 1) {
                    // 削除済み
                    throw new BookOperationException(ErrorTypeEnum.AlreadyDeleted, wBook.Title);
                }
                if (wBook.UserName != null) {
                    // 貸出中
                    throw new BookOperationException(ErrorTypeEnum.DeleteWhileBorrowed, wBook.Title);
                }
                wBook.IsDeleted = 1;
                wRepository.UpdateBook(wBook);
            });
        }

        /// <summary>
        /// 書籍を貸出中にします。
        /// </summary>
        /// <param name="vBookId"></param>
        /// <param name="vUserName"></param>
        public void BorrowBook(int vBookId, string vUserName) {
            this.PerformInTransaction(wRepository => {
                // 書籍情報を取得
                var wBook = wRepository.GetBookById(vBookId);
                if (wBook == null) {
                    // 書籍情報の取得失敗
                    throw new SQLiteException();
                }
                if (wBook.UserName != null) {
                    // 既に貸出中
                    throw new BookOperationException(ErrorTypeEnum.AlreadyBorrowed, wBook.Title);
                }
                wBook.UserName = vUserName;
                wBook.BorrowingDate = DateTime.Now.ToString("yyyy/MM/dd");
                wRepository.UpdateBook(wBook);
            });
        }

        /// <summary>
        /// 書籍を返却します。
        /// </summary>
        /// <param name="vBookId"></param>
        public void ReturnBook(int vBookId) {
            this.PerformInTransaction(wRepository => {
                var wBook = wRepository.GetBookById(vBookId);
                if (wBook == null) {
                    // 書籍情報の取得失敗
                    throw new SQLiteException();
                }
                if (wBook.UserName == null) {
                    // 貸出されていない
                    throw new BookOperationException(ErrorTypeEnum.NotBorrowed, wBook.Title);
                }
                wBook.UserName = null;
                wBook.BorrowingDate = null;
                wRepository.UpdateBook(wBook);
            });
        }

        /// <summary>
        /// 書籍を複数ワードで検索します。
        /// </summary>
        /// <param name="vSearchWords"></param>
        /// <returns>Book</returns>
        public IEnumerable<Book> SearchBooks(IEnumerable<string> vSearchWords) {
            IEnumerable<Book> wBooks = new List<Book>();
            this.PerformInTransaction(wRepository => {
                wBooks = from wBook in wRepository.GetBooks()
                         where vSearchWords.All(wKeyword =>
                             wBook.IsDeleted is 0 &&
                             (wBook.Title.Contains(wKeyword) ||
                             wBook.Author.Contains(wKeyword) ||
                             (wBook.Publisher != null && wBook.Publisher.Contains(wKeyword)) ||
                             (wBook.Description != null && wBook.Description.Contains(wKeyword)) ||
                             (wBook.UserName != null && wBook.UserName.Contains(wKeyword))))
                         orderby wBook.Title
                         select wBook;
            });
            return wBooks;
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose() {

        }
    }
}
