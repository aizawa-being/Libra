using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Libra.Models;
using Moq;
using NUnit.Framework;

namespace LibraUnitTest {
    [TestFixture]
    public class DBUnitTest {

        [Test]
        public void 書籍情報取得テスト() {
            var data = new List<Book> {
                new Book { BookId = 1,
                           Title = "テストタイトル1",
                           Author = "テスト著者1",
                           Barcode = "0000000000001" },
                new Book { BookId = 2,
                           Title = "テストタイトル2",
                           Author = "テスト著者2",
                           Barcode = "0000000000002" },
                new Book { BookId = 3,
                           Title = "テストタイトル3",
                           Author = "テスト著者3",
                           Barcode = "0000000000003" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<BooksDbContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var bookRepository = new BooksRepository(mockContext.Object);
            var books = bookRepository.GetBooks().ToList();

            Assert.AreEqual(3, books.Count());
            Assert.AreEqual("テストタイトル1", books[0].Title);
            Assert.AreEqual("テストタイトル2", books[1].Title);
            Assert.AreEqual("テストタイトル3", books[2].Title);

            Assert.AreEqual("テストタイトル1", bookRepository.GetBookById(1).Title);

            bookRepository.AddBook(new Book { BookId = 4,
                                              Title = "テストタイトル4",
                                              Author = "テスト著者4",
                                              Barcode = "0000000000004" });

            var book = bookRepository.GetBookById(4);
            Assert.AreEqual("テストタイトル4", book.Title);

        }


        public void test() {
            var optionBuilder = new DbContextOptionsBuilder();
        }
    }
}
