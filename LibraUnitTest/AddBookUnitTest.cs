using Libra.Controls;
using Libra.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraUnitTest {
    [TestFixture]
    public class AddBookUnitTest {
        
        [TestCase()]
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
