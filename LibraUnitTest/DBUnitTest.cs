using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Libra.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LibraUnitTest {
    [TestClass]
    public class DBUnitTest {

        [TestMethod]
        public void 書籍情報取得テスト() {
            var data = new List<Book> {
                new Book { Title = "テストタイトル1",
                           Author = "テスト著者1",
                           Barcode = "0000000000001" },
                new Book { Title = "テストタイトル2",
                           Author = "テスト著者2",
                           Barcode = "0000000000002" },
                new Book { Title = "テストタイトル3",
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

            var service = new BooksRepository(mockContext.Object);
            var books = service.GetBooks();
            
            Assert.AreEqual(3, books.Count());
            Assert.AreEqual("テストタイトル1", books.ToList().ElementAt(0).Title);
            Assert.AreEqual("テストタイトル2", books.ToList().ElementAt(1).Title);
            Assert.AreEqual("テストタイトル3", books.ToList().ElementAt(2).Title);
        }
    }
}
