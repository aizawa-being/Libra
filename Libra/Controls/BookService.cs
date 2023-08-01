using System;
using System.Collections.Generic;
using System.Linq;

namespace Libra {

    /// <summary>
    /// 書籍関連のサービスを提供します。
    /// 書籍関連の個別の処理はサービスで実装してください。
    /// </summary>
    public class BookService : IDisposable {
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
            IBookRepository wInsrance = this.FBookRepository();
            var wBooks = from book in wInsrance.GetBooks()
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
            IBookRepository wInsrance = this.FBookRepository();

            wInsrance.BeginTransaction();
            try {
                wInsrance.AddBook(vBook);
                wInsrance.Save();
                wInsrance.CommitTransaction();

                return vBook.BookId;
            } catch (Exception) {
                // ロールバック
                wInsrance.RollbackTransaction();
                throw;
            }
        }
        
        /// 削除フラグを立てます
        /// </summary>
        /// <param name="vBookId"></param>
        public void SetDeleteFlag(int vBookId) {
            IBookRepository wInsrance = this.FBookRepository();
            // トランザクション開始
            wInsrance.BeginTransaction();
            try {

                var wBook = wInsrance.GetBookById(vBookId);
                if (wBook.IsDeleted == 1) {
                    // 削除済み
                    throw new BookOperationException(ErrorTypeEnum.AlreadyDeleted, wBook.Title);
                }
                if (wBook.UserName != null) {
                    // 貸出中
                    throw new BookOperationException(ErrorTypeEnum.IsBorrowed, wBook.Title);
                }
                wBook.IsDeleted = 1;
                wInsrance.UpdateBook(wBook);
                wInsrance.Save();
                wInsrance.CommitTransaction();

            } catch (Exception) {
                // ロールバック
                wInsrance.RollbackTransaction();
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
