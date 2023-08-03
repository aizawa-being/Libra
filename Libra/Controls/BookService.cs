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
                    throw new BookOperationException(ErrorTypeEnum.IsBorrowed, wBook.Title);
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
        /// 書籍を複数ワードで検索します。
        /// </summary>
        /// <param name="vSearchWords"></param>
        /// <returns>Book</returns>
        public IEnumerable<Book> SearchBooks(IEnumerable<string> vSearchWords) {
            IBookRepository wInstance = this.FBookRepository();
            var wBooks = from wBook in wInstance.GetBooks()
                         where vSearchWords.All(wKeyword =>
                             wBook.Title.Contains(wKeyword) ||
                             wBook.Author.Contains(wKeyword) ||
                             (wBook.Publisher != null && wBook.Publisher.Contains(wKeyword)) ||
                             (wBook.Description != null && wBook.Description.Contains(wKeyword)) ||
                             (wBook.UserName != null && wBook.UserName.Contains(wKeyword)) &&
                             wBook.IsDeleted is 0)
                         orderby wBook.Title
                         select wBook;
            return wBooks;
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose() {

        }
    }
}
