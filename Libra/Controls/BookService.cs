using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

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
        /// 削除されていない書籍情報を全て取得します。
        /// </summary>
        /// <returns>books</returns>
        public IEnumerable<Book> GetExistBooks() {
            IBookRepository wInstance = this.FBookRepository();
            var wBooks = from book in wInstance.GetBooks()
                         where book.IsDeleted is 0
                         orderby book.Title
                         select book;
            return wBooks;
        }

        /// <summary>
        /// 書籍情報をDBに追加し、自動採番された書籍IDを通知します。
        /// </summary>
        /// <param name="vBook"></param>
        /// <returns>int</returns>
        public int AddBook(Book vBook) {
            IBookRepository wInstance = this.FBookRepository();

            wInstance.BeginTransaction();
            try {
                wInstance.AddBook(vBook);
                wInstance.Save();
                wInstance.CommitTransaction();

                return vBook.BookId;
            } catch (Exception) {
                // ロールバック
                wInstance.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// 削除フラグを立てます
        /// </summary>
        /// <param name="vBookId"></param>
        public void SetDeleteFlag(int vBookId) {
            IBookRepository wInstance = this.FBookRepository();
            // トランザクション開始
            wInstance.BeginTransaction();
            try {
                var wBook = wInstance.GetBookById(vBookId);
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
                wInstance.UpdateBook(wBook);
                wInstance.Save();
                wInstance.CommitTransaction();

            } catch (Exception) {
                // ロールバック
                wInstance.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// 書籍を貸出中にします。
        /// </summary>
        /// <param name="vBookId"></param>
        /// <param name="vUserName"></param>
        public void BorrowBook(int vBookId, string vUserName) {
            IBookRepository wInstance = this.FBookRepository();
            // トランザクション開始
            wInstance.BeginTransaction();
            try {
                var wBook = wInstance.GetBookById(vBookId);
                if (wBook == null) {
                    throw new SQLiteException();
                }
                if (wBook.UserName != null) {
                    // 貸出中
                    throw new BookOperationException(ErrorTypeEnum.AlreadyBorrowed, wBook.Title);
                }
                wBook.UserName = vUserName;
                wInstance.UpdateBook(wBook);
                wInstance.Save();
                wInstance.CommitTransaction();

            } catch (Exception) {
                // ロールバック
                wInstance.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// 書籍を返却します。
        /// </summary>
        /// <param name="vBookId"></param>
        /// <param name="vUserName"></param>
        public void ReturnBook(int vBookId) {
            IBookRepository wInstance = this.FBookRepository();
            // トランザクション開始
            wInstance.BeginTransaction();
            try {
                var wBook = wInstance.GetBookById(vBookId);
                if (wBook == null) {
                    throw new SQLiteException();
                }
                if (wBook.UserName == null) {
                    // 貸出されていない
                    throw new BookOperationException(ErrorTypeEnum.NotBorrowed, wBook.Title);
                }
                wBook.UserName = null;
                wInstance.UpdateBook(wBook);
                wInstance.Save();
                wInstance.CommitTransaction();

            } catch (Exception) {
                // ロールバック
                wInstance.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose() {

        }
    }
}
