using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

using Libra;

namespace LibraUnitTest {
    [TestFixture]
    public class DBInterfaceUnitTest {
        private readonly Dictionary<int, Book> Books = new Dictionary<int, Book>() {
            {
                1, new Book {
                    BookId = 1,
                    Title = "テストタイトル1",
                    Author = "テスト著者1",
                    Barcode = "0000000000001",
                    IsDeleted = 0 }
            },
            {
                2, new Book {
                    BookId = 2,
                    Title = "テストタイトル2",
                    Author = "テスト著者2",
                    Barcode = "0000000000002",
                    IsDeleted = 0 }
            },
            {
                3, new Book {
                    BookId = 3,
                    Title = "テストタイトル3",
                    Author = "テスト著者3",
                    Barcode = "0000000000003",
                    IsDeleted = 0 }
            }
        };

        [Test]
        public void 書籍削除テスト() {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(true)) {
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
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(true)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBooks = wBookRepository.GetBooks().ToList();

                Assert.AreEqual(3, wBooks.Count());
                Assert.AreEqual("テストタイトル1", wBooks[0].Title);
                Assert.AreEqual("テストタイトル2", wBooks[1].Title);
                Assert.AreEqual("テストタイトル3", wBooks[2].Title);
            }
        }

        [Test]
        public void テーブルにレコードが存在しない場合に書籍情報を全取得するテスト() {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBooks = wBookRepository.GetBooks().ToList();

                Assert.IsNotNull(wBooks);
                Assert.AreEqual(0, wBooks.Count());
            }
        }

        [TestCase(1)]
        public void 書籍1冊取得テスト(int vBookId) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(true)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBook = wBookRepository.GetBookById(vBookId);

                Assert.AreEqual(vBookId, wBook.BookId);
                Assert.AreEqual(this.Books[1].Title, wBook.Title);
                Assert.AreEqual(this.Books[1].Author, wBook.Author);
                Assert.AreEqual(this.Books[1].Barcode, wBook.Barcode);
                Assert.AreEqual(this.Books[1].IsDeleted, wBook.IsDeleted);
            }
        }

        [Test]
        public void テーブルにレコードが存在しない場合に書籍情報を1件取得するテスト() {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBook = wBookRepository.GetBookById(1);

                Assert.IsNull(wBook);
            }
        }

        [TestCase(0)]
        [TestCase(null)]
        public void 存在しないIDの書籍取得テスト(int vBookId) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(true)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBook = wBookRepository.GetBookById(vBookId);

                Assert.IsNull(wBook);
            }
        }

        [Test]
        public void 書籍追加テスト() {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(true)) {
                var wBooksRepository = new BookRepository(wDbContext);

                wBooksRepository.AddBook(new Book {
                    BookId = 4,
                    Title = "AddBook書籍名",
                    Author = "AddBook著者名",
                    Barcode = "0000000000001",
                    IsDeleted = 0
                });
                wBooksRepository.Save();

                Assert.AreEqual(4, wDbContext.Books.Count());
                Assert.AreEqual(4, wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().BookId);
                Assert.AreEqual("AddBook書籍名", wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Title);
                Assert.AreEqual("AddBook著者名", wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Author);
                Assert.AreEqual("0000000000001", wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Barcode);
                Assert.AreEqual(0, wDbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().IsDeleted);
            }
        }

        [Test]
        public void 書籍更新テスト() {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(true)) {
                var wBookRepository = new BookRepository(wDbContext);

                // 書籍IDが1の書籍を取り出す
                var wBook = wBookRepository.GetBookById(1);

                // 書籍名を更新
                wBook.Title = "UpdatedTitle";
                wBookRepository.UpdateBook(wBook);

                var wUpdatedBook = wBookRepository.GetBookById(1);

                Assert.AreEqual("UpdatedTitle", wUpdatedBook.Title);
            }
        }
    }
}