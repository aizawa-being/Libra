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
    public class DBUnitTest {
        
        [Test]
        public void 書籍DB用インターフェーステスト() {
            
            // メモリ上にDBを構築する
            string connectionString = "Data Source=:memory:;Version=3;New=True;";
            DbConnection connection = new SQLiteConnection(connectionString);
            
            connection.Open();
            
            BooksDbContext dbContext = new BooksDbContext(connection, contextOwnsConnection: true);
            
            if (!dbContext.Database.Exists()) {
                dbContext.Database.Create();
            }

            var bookRepository = new BooksRepository((BooksDbContext)dbContext);

            // 書籍追加
            bookRepository.AddBook(new Book {
                BookId = 1,
                Title = "テストタイトル1",
                Author = "テスト著者1",
                Barcode = "0000000000001",
                IsDeleted = 0
            });
            bookRepository.Save();

            // 書籍全件取得
            var books = bookRepository.GetBooks().ToList();

            // 追加･全取得できたかのテスト
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("テストタイトル1", books[0].Title);
            
            // ID指定で書籍1冊取得
            var book = bookRepository.GetBookById(1);

            Assert.AreEqual("テストタイトル1", book.Title);

            book.Title = "Updatedタイトル";
            
            // 書籍のタイトルを更新して保存。
            bookRepository.UpdateBook(book);
            bookRepository.Save();

            var updateBooks = bookRepository.GetBooks().ToList();
            connection.Close();

            // 更新できたかテスト
            Assert.AreEqual(1, updateBooks.Count());
            Assert.AreEqual("Updatedタイトル", updateBooks[0].Title);
        }
    }
}
