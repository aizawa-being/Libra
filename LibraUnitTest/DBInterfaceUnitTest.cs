using System.Linq;

using NUnit.Framework;

using Libra;

namespace LibraUnitTest {
    [TestFixture]
    public class DBInterfaceUnitTest {

        [Test]
        public void 書籍削除テスト() {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb()) {
                var wBookRepository = new BookRepository(wDbContext);

                var wBooks = wBookRepository.GetBooks();

                wBookRepository.DeleteBook(1);
                wBookRepository.Save();

                var wDeletedBooks = wBookRepository.GetBooks().ToList();

                Assert.AreEqual(3, wBooks.Count());
                Assert.AreEqual(2, wDeletedBooks.Count());
                Assert.AreEqual(false, wDeletedBooks.Exists(b => b.BookId.Equals(1)));
                Assert.AreEqual(true, wDeletedBooks.Exists(b => b.BookId.Equals(2)));
                Assert.AreEqual(true, wDeletedBooks.Exists(b => b.BookId.Equals(3)));
            }
        }

        [Test]
        public void 書籍全取得テスト() {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb()) {

                var wBooksRepository = new BookRepository(wDbContext);

                var wBooks = wBooksRepository.GetBooks().ToList();

                Assert.AreEqual(3, wBooks.Count());
                Assert.AreEqual("テストタイトル1", wBooks[0].Title);
                Assert.AreEqual("テストタイトル2", wBooks[1].Title);
                Assert.AreEqual("テストタイトル3", wBooks[2].Title);
            }

        }

        [Test]
        public void 書籍1冊取得テスト() {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb()) {
                var wBookRepository = new BookRepository(wDbContext);

                var wBook = wBookRepository.GetBookById(1);

                Assert.AreEqual(1, wBook.BookId);
                Assert.AreEqual("テストタイトル1", wBook.Title);
                Assert.AreEqual("テスト著者1", wBook.Author);
                Assert.AreEqual("0000000000001", wBook.Barcode);
                Assert.AreEqual(0, wBook.IsDeleted);
            }
        }

        [TestCase(4, "AddBook書籍名", "AddBook著者名", "0000000000001", 0)]
        public void 書籍追加テスト(int vBookId, string vTitle, string vAuthor, string vBarcode, int vIsDeleted) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb()) {
                var wBookRepository = new BookRepository(wDbContext);
                wBookRepository.AddBook(new Book {
                    BookId = vBookId,
                    Title = "AddBook書籍名",
                    Author = "AddBook著者名",
                    Barcode = "0000000000001",
                    IsDeleted = 0
                });
                wBookRepository.Save();
                
                Assert.AreEqual(4, wDbContext.Books.Count());
                Assert.AreEqual(vBookId, wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().BookId);
                Assert.AreEqual(vTitle, wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Title);
                Assert.AreEqual(vAuthor, wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Author);
                Assert.AreEqual(vBarcode, wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Barcode);
                Assert.AreEqual(vIsDeleted, wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().IsDeleted);
            }
        }

        [TestCase("UpdatedTitle")]
        public void 書籍更新テスト(string vTitle) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb()) {
                var wBookRepository = new BookRepository(wDbContext);

                // 1冊取り出す
                var wBook = wBookRepository.GetBookById(1);

                // タイトルを更新
                wBook.Title = vTitle;
                wBookRepository.UpdateBook(wBook);

                var wUpdatedBook = wBookRepository.GetBookById(1);

                Assert.AreEqual(vTitle, wUpdatedBook.Title);
            }
        }
    }
}
