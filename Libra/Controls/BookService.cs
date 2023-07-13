namespace Libra {
    public class BookService {
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
            this.FBooksRepository.AddBook(vBook);
            this.FBooksRepository.Save();
            return vBook.BookId;
        }
    }
}
