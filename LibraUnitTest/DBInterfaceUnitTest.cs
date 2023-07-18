using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using System.Data.SQLite;
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
                    IsDeleted = 0
                }
            }
        };

        /// <summary>
        /// インメモリデータベースを構築します。
        /// </summary>
        /// <param name="vIsDataSet">true:初期データを設定する</param>
        /// <returns></returns>
        public BooksDbContext CreateInMemoryDb(bool vIsDataSet) {
            // メモリ上にDBを構築する
            var wConnectionString = "Data Source=:memory:;Version=3;New=True;";
            var wConnection = new SQLiteConnection(wConnectionString);

            wConnection.Open();

            var wDbContext = new BooksDbContext(wConnection, true);
            var wTableExists = wDbContext.Database.SqlQuery<int>("SELECT 1 FROM sqlite_master WHERE type='table' AND name='Book'").Any();

            // テーブルが存在しない場合、テーブルの再定義
            if (!wTableExists) {
                wDbContext.Database.ExecuteSqlCommand(@"
                    CREATE TABLE Book (
                        BookId INTEGER PRIMARY KEY,
                        Title TEXT,
                        Author TEXT,
                        Publisher TEXT,
                        Description TEXT,
                        Barcode TEXT,
                        IsDeleted INTEGER,
                        UserName TEXT,
                        BorrowingDate TEXT
                    )");
            }
            if (vIsDataSet) {
                this.SetDefaultBooks(wDbContext);
            }
            wDbContext.SaveChanges();

            return wDbContext;
        }

        /// <summary>
        /// DBに初期データを格納します。
        /// </summary>
        /// <param name="vDbContext"></param>
        private void SetDefaultBooks(BooksDbContext vDbContext) {
            vDbContext.Books.Add(this.Books[1]);
            vDbContext.Books.Add(this.Books[2]);
            vDbContext.Books.Add(this.Books[3]);
        }

        [Test]
        public void 書籍削除テスト() {
            using (var wDbContext = CreateInMemoryDb(true)) {
                var wBooksRepository = new BookRepository(wDbContext);
                var wBooks = wBooksRepository.GetBooks();

                // 書籍IDが1の書籍を削除
                wBooksRepository.DeleteBook(1);
                wBooksRepository.Save();

                var wDeletedBooks = wBooksRepository.GetBooks().ToList();

                Assert.AreEqual(3, wBooks.Count());
                Assert.AreEqual(2, wDeletedBooks.Count());
                Assert.AreEqual(false, wDeletedBooks.Exists(b => b.BookId.Equals(1)));
                Assert.AreEqual(true, wDeletedBooks.Exists(b => b.BookId.Equals(2)));
                Assert.AreEqual(true, wDeletedBooks.Exists(b => b.BookId.Equals(3)));
            }
        }

        [Test]
        public void 書籍全取得テスト() {
            using (var wDbContext = CreateInMemoryDb(true)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBooks = wBookRepository.GetBooks().ToList();

                Assert.AreEqual(3, wBooks.Count());
                Assert.AreEqual(this.Books[1].Title, wBooks[0].Title);
                Assert.AreEqual(this.Books[2].Title, wBooks[1].Title);
                Assert.AreEqual(this.Books[3].Title, wBooks[2].Title);
            }
        }

        [Test]
        public void テーブルにレコードが存在しない場合に書籍情報を全取得するテスト() {
            using (var wDbContext = CreateInMemoryDb(false)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBooks = wBookRepository.GetBooks().ToList();

                Assert.IsNotNull(wBooks);
                Assert.AreEqual(0, wBooks.Count());
            }
        }

        [TestCase(1)]
        public void 書籍1冊取得テスト(int vBookId) {
            using (var wDbContext = CreateInMemoryDb(true)) {
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
            using (var wDbContext = CreateInMemoryDb(false)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBook = wBookRepository.GetBookById(1);

                Assert.IsNull(wBook);
            }
        }

        [TestCase(0)]
        [TestCase(null)]
        public void 存在しないIDの書籍取得テスト(int vBookId) {
            using (var wDbContext = CreateInMemoryDb(true)) {
                var wBookRepository = new BookRepository(wDbContext);
                var wBook = wBookRepository.GetBookById(vBookId);

                Assert.IsNull(wBook);
            }
        }

        [Test]
        public void 書籍追加テスト() {
            using (var wDbContext = CreateInMemoryDb(true)) {
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
            using (var wDbContext = CreateInMemoryDb(true)) {
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
