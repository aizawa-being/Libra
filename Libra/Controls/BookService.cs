using System;

namespace Libra {
    public class BookService : IDisposable {
        private IBookRepository FBooksRepository;

        public BookService() {
            this.FBooksRepository = new BooksRepository(new BooksDbContext());
        }

        public BookService(IBookRepository vBookRepository) {
            this.FBooksRepository = vBookRepository;
        }

        /// <summary>
        /// 書籍情報をDBに追加し、自動採番された書籍IDを通知します。
        /// </summary>
        /// <param name="vBook"></param>
        /// <returns>int</returns>
        public int AddBook(Book vBook) {
            this.FBooksRepository.BeginTransaction();
            try {
                this.FBooksRepository.AddBook(vBook);
                this.FBooksRepository.Save();
                this.FBooksRepository.CommitTransaction();

                return vBook.BookId;

            } catch (Exception) {
                // ロールバック
                this.FBooksRepository.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose() {
            this.FBooksRepository.Dispose();
        }
    }
}
