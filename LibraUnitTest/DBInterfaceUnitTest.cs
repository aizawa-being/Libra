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

        [Test]
        public void 書籍削除テスト() {
            var createDb = new CreateBooksDb();
            using (var dbContext = createDb.CreateInMemoryDb()) {
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
            var createDb = new CreateBooksDb();
            using (var dbContext = createDb.CreateInMemoryDb()) {

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
            var createDb = new CreateBooksDb();
            using (var dbContext = createDb.CreateInMemoryDb()) {
                var booksRepository = new BooksRepository(dbContext);

                var book = booksRepository.GetBookById(1);

                Assert.AreEqual(1, book.BookId);
                Assert.AreEqual("テストタイトル1", book.Title);
                Assert.AreEqual("テスト著者1", book.Author);
                Assert.AreEqual("0000000000001", book.Barcode);
                Assert.AreEqual(0, book.IsDeleted);
            }
        }

        [TestCase(4, "AddBook書籍名", "AddBook著者名", "0000000000001", 0)]
        public void 書籍追加テスト(int vBookId, string vTitle, string vAuthor, string vBarcode, int vIsDeleted) {
            var createDb = new CreateBooksDb();
            using (var dbContext = createDb.CreateInMemoryDb()) {
                var booksRepository = new BooksRepository(dbContext);
                booksRepository.AddBook(new Book {
                    BookId = vBookId,
                    Title = "AddBook書籍名",
                    Author = "AddBook著者名",
                    Barcode = "0000000000001",
                    IsDeleted = 0
                });
                booksRepository.Save();
                
                Assert.AreEqual(4, dbContext.Books.Count());
                Assert.AreEqual(vBookId, dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().BookId);
                Assert.AreEqual(vTitle, dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Title);
                Assert.AreEqual(vAuthor, dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Author);
                Assert.AreEqual(vBarcode, dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().Barcode);
                Assert.AreEqual(vIsDeleted, dbContext.Books.OrderByDescending(b => b.BookId).FirstOrDefault().IsDeleted);
            }
        }

        [TestCase("UpdatedTitle")]
        public void 書籍更新テスト(string vTitle) {
            var createDb = new CreateBooksDb();
            using (var dbContext = createDb.CreateInMemoryDb()) {
                var booksRepository = new BooksRepository(dbContext);

                // 1冊取り出す
                var book = booksRepository.GetBookById(1);

                // タイトルを更新
                book.Title = vTitle;
                booksRepository.UpdateBook(book);

                var updatedBook = booksRepository.GetBookById(1);

                Assert.AreEqual(vTitle, updatedBook.Title);
            }
        }

    }
}
