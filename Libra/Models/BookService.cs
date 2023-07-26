using System;
using System.Collections.Generic;
using System.Linq;

namespace Libra {

    /// <summary>
    /// 書籍関連のサービスを提供します。
    /// 書籍関連の個別の処理はサービスで実装してください。
    /// </summary>
    public class BookService : IDisposable {
        private readonly IBookRepository FBookRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BookService () {
            this.FBookRepository = new BookRepository(new BooksDbContext());
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vBookRepository"></param>
        public BookService(IBookRepository vBookRepository) {
            this.FBookRepository = vBookRepository;
        }

        /// <summary>
        /// 削除されていない書籍情報を全て取得します。
        /// </summary>
        /// <returns>books</returns>
        public IEnumerable<Book> GetExistBooks() {
            var wBooks = from book in FBookRepository.GetBooks()
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
            this.FBookRepository.BeginTransaction();
            try {
                this.FBookRepository.AddBook(vBook);
                this.FBookRepository.Save();
                this.FBookRepository.CommitTransaction();

                return vBook.BookId;
            } catch (Exception) {
                // ロールバック
                this.FBookRepository.RollbackTransaction();
                throw;
            }
        }
        
        /// 削除フラグを立てます
        /// </summary>
        /// <param name="vBookId"></param>
        public void SetDeleteFlag(int vBookId) {
            // トランザクション開始
            this.FBookRepository.BeginTransaction();
            try {

                var wBook = this.FBookRepository.GetBookById(vBookId);
                if (wBook.IsDeleted == 1) {
                    // 削除済み
                    throw new BookOperationException(ErrorTypeEnum.AlreadyDeleted, wBook.Title);
                }
                if (wBook.UserName != null) {
                    // 貸出中
                    throw new BookOperationException(ErrorTypeEnum.IsBorrowed, wBook.Title);
                }
                wBook.IsDeleted = 1;
                this.FBookRepository.UpdateBook(wBook);
                this.FBookRepository.Save();
                this.FBookRepository.CommitTransaction();

            } catch (Exception) {
                // ロールバック
                this.FBookRepository.RollbackTransaction();
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
