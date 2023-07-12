using Libra.Models;

namespace Libra.Controls {
    public class BookService {
        private IBookRepository F_BooksRepository;

        public BookService() {
            this.F_BooksRepository = new BooksRepository(new BooksDbContext());
        }

        public BookService(IBookRepository vBookRepository) {
            this.F_BooksRepository = vBookRepository;
        }

        /// <summary>
        /// 書籍情報をDbに登録し、自動採番された書籍IDを通知します。
        /// </summary>
        /// <param name="vBook"></param>
        /// <returns>int</returns>
        public int AddBook(Book vBook) {
            this.F_BooksRepository.AddBook(vBook);
            this.F_BooksRepository.Save();
            return vBook.BookId;
        }
    }
}
