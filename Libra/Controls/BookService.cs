using System;

namespace Libra {
    public class BookService : IDisposable {
        private readonly IBookRepository FBookRepository;

        public BookService() {
            this.FBookRepository = new BookRepository(new BooksDbContext());
        }

        public BookService(IBookRepository vBookRepository) {
            this.FBookRepository = vBookRepository;
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

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose() {
            this.FBookRepository.Dispose();
        }
    }
}
