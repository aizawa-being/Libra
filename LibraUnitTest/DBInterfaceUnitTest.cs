using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using Libra.Models;
using Moq;
using NUnit.Framework;

namespace LibraUnitTest {
    [TestFixture]
    public class DBInterfaceUnitTest {

        /// <summary>
        /// インメモリデータベースを構築します。
        /// </summary>
        /// <returns></returns>
        public BooksDbContext CreateInMemoryDb() {
            // メモリ上にDBを構築する
            string connectionString = "Data Source=:memory:;Version=3;New=True;";

            var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var dbContext = new BooksDbContext(connection, true);

            bool tableExists = dbContext.Database.SqlQuery<int>("SELECT 1 FROM sqlite_master WHERE type='table' AND name='Book'").Any();

            if (!tableExists) {
                dbContext.Database.ExecuteSqlCommand(@"
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
            SetDefaultBooks(dbContext);
            dbContext.SaveChanges();

            return dbContext;
        }

        /// <summary>
        /// DBに初期データを格納します。
        /// </summary>
        /// <param name="vDbContext"></param>
        private void SetDefaultBooks(BooksDbContext vDbContext) {
            vDbContext.Books.Add(new Book {
                BookId = 1,
                Title = "テストタイトル1",
                Author = "テスト著者1",
                Barcode = "0000000000001",
                IsDeleted = 0
            });
            vDbContext.Books.Add(new Book {
                BookId = 2,
                Title = "テストタイトル2",
                Author = "テスト著者2",
                Barcode = "0000000000002",
                IsDeleted = 0
            });
            vDbContext.Books.Add(new Book {
                BookId = 3,
                Title = "テストタイトル3",
                Author = "テスト著者3",
                Barcode = "0000000000003",
                IsDeleted = 0
            });
        }

        [Test]
        public void 書籍削除テスト() {
            using (var dbContext = CreateInMemoryDb()) {
                var booksRepository = new BooksRepository(dbContext);

                var books = booksRepository.GetBooks();

                booksRepository.DeleteBook(1);
                booksRepository.Save();

                var deletedBooks = booksRepository.GetBooks().ToList();

                Assert.AreEqual(3, books.Count());
                Assert.AreEqual(2, deletedBooks.Count());
                Assert.AreEqual(false, deletedBooks.Exists(b => b.BookId.Equals(1)));
                Assert.AreEqual(true, deletedBooks.Exists(b => b.BookId.Equals(2)));
                Assert.AreEqual(true, deletedBooks.Exists(b => b.BookId.Equals(3)));
            }
        }

        [Test]
        public void 書籍全取得テスト() {
            using (var dbContext = CreateInMemoryDb()) {

                var booksRepository = new BooksRepository(dbContext);

                var books = booksRepository.GetBooks().ToList();

                Assert.AreEqual(3, books.Count());
                Assert.AreEqual("テストタイトル1", books[0].Title);
                Assert.AreEqual("テストタイトル2", books[1].Title);
                Assert.AreEqual("テストタイトル3", books[2].Title);
            }

        }

        [Test]
        public void 書籍1冊取得テスト() {

            using (var dbContext = CreateInMemoryDb()) {
                var booksRepository = new BooksRepository(dbContext);

                var book = booksRepository.GetBookById(1);

                Assert.AreEqual(1, book.BookId);
                Assert.AreEqual("テストタイトル1", book.Title);
                Assert.AreEqual("テスト著者1", book.Author);
                Assert.AreEqual("0000000000001", book.Barcode);
                Assert.AreEqual(0, book.IsDeleted);
            }
        }

        [Test]
        public void 書籍追加テスト() {
            using (var dbContext = CreateInMemoryDb()) {
                var booksRepository = new BooksRepository(dbContext);
                booksRepository.AddBook(new Book {
                    BookId = 4,
                    Title = "AddBook書籍名",
                    Author = "AddBook著者名",
                    Barcode = "0000000000001",
                    IsDeleted = 0
                });
                booksRepository.Save();

                Assert.AreEqual(4, dbContext.Books.Count());
                Assert.AreEqual(4, dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().BookId);
                Assert.AreEqual("AddBook書籍名", dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Title);
                Assert.AreEqual("AddBook著者名", dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Author);
                Assert.AreEqual("0000000000001", dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Barcode);
                Assert.AreEqual(0, dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().IsDeleted);
            }
        }

        [Test]
        public void 書籍更新テスト() {
            using (var dbContext = CreateInMemoryDb()) {
                var booksRepository = new BooksRepository(dbContext);

                // 1冊取り出す
                var book = booksRepository.GetBookById(1);

                // タイトルを更新
                book.Title = "UpdatedTitle";
                booksRepository.UpdateBook(book);

                var updatedBook = booksRepository.GetBookById(1);

                Assert.AreEqual("UpdatedTitle", updatedBook.Title);
            }
        }

    }
}
