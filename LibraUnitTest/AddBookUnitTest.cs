using Libra;
using Moq;
using NUnit.Framework;

namespace LibraUnitTest {
    [TestFixture]
    public class AddBookUnitTest {
        
        public void DBに書籍を追加するテスト(string vTitle, string vAuthor, string vPublisher, string vDescription, string vBarcode) {
            var createDb = new CreateBooksDb();
            using (var dbContext = createDb.CreateInMemoryDb()) {
                var booksRepositoryMock = new Mock<IBookRepository>();

                var bookService = new BookService();
                var addBook = new Book{
                    Title = vTitle,
                    Author = vAuthor,
                    Publisher = vPublisher,
                    Description = vDescription,
                    Barcode = vBarcode
                };

                var addBookId = bookService.AddBook(addBook);
                
            }
        }
    }
}
