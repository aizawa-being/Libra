using System;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using System.Data.SQLite;

using Libra;

namespace LibraUnitTest {
    [TestFixture]
    public class RentalBookTest {
        private readonly Dictionary<int, Book> FBooks = new Dictionary<int, Book>() {
            {
                1, new Book {
                    BookId = 1,
                    Title = "テストタイトル1",
                    Author = "テスト著者1",
                    Publisher = "テスト出版社1",
                    Description = "テスト概要1",
                    UserName = null,
                    Barcode = "0000000000001",
                    IsDeleted = 0,
                    BorrowingDate = null }
            },
            {
                2, new Book {
                    BookId = 2,
                    Title = "テストタイトル2",
                    Author = "テスト著者2",
                    Publisher = "テスト出版社2",
                    Description = "テスト概要2",
                    UserName = "テスト利用者名2",
                    Barcode = "0000000000002",
                    IsDeleted = 0,
                    BorrowingDate = "2023/01/01" }
            },
            {
                3, new Book {
                    BookId = 3,
                    Title = "テストタイトル3",
                    Author = "テスト著者3",
                    Publisher = "テスト出版社3",
                    Description = "テスト概要3",
                    UserName = "テスト利用者名3",
                    Barcode = "0000000000003",
                    IsDeleted = 0,
                    BorrowingDate = "2023/01/01" }
            }
        };
        
        [TestCase("テスト利用者名1")]
        public void BooksServiceで貸出中にするテスト(string vUserName) {

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                for (int i = 1; i <= this.FBooks.Count; i++) {
                    wDbContext.Books.Add(new Book {
                        BookId = this.FBooks[i].BookId,
                        Title = this.FBooks[i].Title,
                        Author = this.FBooks[i].Author,
                        Publisher = this.FBooks[i].Publisher,
                        Description = this.FBooks[i].Description,
                        Barcode = this.FBooks[i].Barcode,
                        IsDeleted = this.FBooks[i].IsDeleted,
                        UserName = this.FBooks[i].UserName,
                        BorrowingDate = this.FBooks[i].BorrowingDate
                    });
                }
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);

                wBookService.BorrowBook(1, vUserName);

                var wBook = wDbContext.Books.Find(1);

                Assert.AreEqual(vUserName, wBook.UserName);
                Assert.IsNotNull(wBook.BorrowingDate);
            }
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("　")]
        [TestCase("テスト利用者名2")]
        [TestCase("テスト利用者名3")]
        public void BooksServiceで貸出処理中に既に貸出中エラー発生テスト(string vUserName) {

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                for (int i = 1; i <= this.FBooks.Count; i++) {
                    wDbContext.Books.Add(new Book {
                        BookId = this.FBooks[i].BookId,
                        Title = this.FBooks[i].Title,
                        Author = this.FBooks[i].Author,
                        Publisher = this.FBooks[i].Publisher,
                        Description = this.FBooks[i].Description,
                        Barcode = this.FBooks[i].Barcode,
                        IsDeleted = this.FBooks[i].IsDeleted,
                        UserName = this.FBooks[i].UserName,
                        BorrowingDate = this.FBooks[i].BorrowingDate
                    });
                }
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);

                var wException = Assert.Throws<BookOperationException>(() => wBookService.BorrowBook(2, vUserName));
                Assert.AreEqual(ErrorTypeEnum.AlreadyBorrowed, wException.ErrorType);

                var wBook = wDbContext.Books.Find(2);

                Assert.IsNotNull(wBook.UserName);
                Assert.IsNotNull(wBook.BorrowingDate);
            }
        }

        [Test]
        public void BooksServiceで貸出時存在しない書籍IDを指定エラー発生テスト() {

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                for (int i = 1; i <= this.FBooks.Count; i++) {
                    wDbContext.Books.Add(new Book {
                        BookId = this.FBooks[i].BookId,
                        Title = this.FBooks[i].Title,
                        Author = this.FBooks[i].Author,
                        Publisher = this.FBooks[i].Publisher,
                        Description = this.FBooks[i].Description,
                        Barcode = this.FBooks[i].Barcode,
                        IsDeleted = this.FBooks[i].IsDeleted,
                        UserName = this.FBooks[i].UserName,
                        BorrowingDate = this.FBooks[i].BorrowingDate
                    });
                }
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);

                Assert.Throws<SQLiteException>(() => wBookService.BorrowBook(4, string.Empty));
            }
        }

        [TestCase(2)]
        [TestCase(3)]
        public void BooksServiceで返却するテスト(int vBookId) {

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                for (int i = 1; i <= this.FBooks.Count; i++) {
                    wDbContext.Books.Add(new Book {
                        BookId = this.FBooks[i].BookId,
                        Title = this.FBooks[i].Title,
                        Author = this.FBooks[i].Author,
                        Publisher = this.FBooks[i].Publisher,
                        Description = this.FBooks[i].Description,
                        Barcode = this.FBooks[i].Barcode,
                        IsDeleted = this.FBooks[i].IsDeleted,
                        UserName = this.FBooks[i].UserName,
                        BorrowingDate = this.FBooks[i].BorrowingDate
                    });
                }
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);

                wBookService.ReturnBook(vBookId);

                var wBook = wDbContext.Books.Find(vBookId);

                Assert.IsNull(wBook.UserName);
                Assert.IsNull(wBook.BorrowingDate);
            }
        }

        [TestCase(1)]
        public void BooksServiceで返却処理中に貸出されていないエラー発生テスト(int vBookId) {

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                for (int i = 1; i <= this.FBooks.Count; i++) {
                    wDbContext.Books.Add(new Book {
                        BookId = this.FBooks[i].BookId,
                        Title = this.FBooks[i].Title,
                        Author = this.FBooks[i].Author,
                        Publisher = this.FBooks[i].Publisher,
                        Description = this.FBooks[i].Description,
                        Barcode = this.FBooks[i].Barcode,
                        IsDeleted = this.FBooks[i].IsDeleted,
                        UserName = this.FBooks[i].UserName,
                        BorrowingDate = this.FBooks[i].BorrowingDate
                    });
                }
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);

                var wException = Assert.Throws<BookOperationException>(() => wBookService.ReturnBook(1));
                Assert.AreEqual(ErrorTypeEnum.NotBorrowed, wException.ErrorType);

                var wBook = wDbContext.Books.Find(1);

                Assert.IsNull(wBook.UserName);
                Assert.IsNull(wBook.BorrowingDate);
            }
        }

        [Test]
        public void BooksServiceで返却時存在しない書籍IDを指定エラー発生テスト() {

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                for (int i = 1; i <= this.FBooks.Count; i++) {
                    wDbContext.Books.Add(new Book {
                        BookId = this.FBooks[i].BookId,
                        Title = this.FBooks[i].Title,
                        Author = this.FBooks[i].Author,
                        Publisher = this.FBooks[i].Publisher,
                        Description = this.FBooks[i].Description,
                        Barcode = this.FBooks[i].Barcode,
                        IsDeleted = this.FBooks[i].IsDeleted,
                        UserName = this.FBooks[i].UserName,
                        BorrowingDate = this.FBooks[i].BorrowingDate
                    });
                }
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);

                Assert.Throws<SQLiteException>(() => wBookService.ReturnBook(4));
            }
        }

        [Test]
        public void LibraControlで書籍を貸出中にするテスト() {
            // BookRepositoryのモック作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Returns(new Book {
                UserName = null,
                BorrowingDate = null
            });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));

            // メッセージボックスのモック作成
            var wMockMessageService = new Mock<IMessageBoxUtil>();
            wMockMessageService.Setup(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()));

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMockMessageService.Object);
            wLibraControl.BorrowBook("test", It.IsAny<int>());

            wMockRepository.Verify(m => m.CommitTransaction(), Times.Once);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Never);
            wMockMessageService.Verify(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()), Times.Never);
        }

        [Test]
        public void LibraControlで貸出処理中に既に貸出中エラー発生テスト() {
            // BookRepositoryのモック作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Returns(new Book {
                UserName = "test",
                BorrowingDate = "2023/01/01"
            });

            // メッセージボックスのモック作成
            var wMockMessageService = new Mock<IMessageBoxUtil>();
            wMockMessageService.Setup(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()));

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMockMessageService.Object);
            wLibraControl.BorrowBook("test", It.IsAny<int>());

            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
            wMockMessageService.Verify(m => m.Show(MessageTypeEnum.AlreadyBorrowed, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlで貸出処理中にDBエラー発生テスト() {
            // BookRepositoryのモック作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Throws<SQLiteException>();

            // メッセージボックスのモック作成
            var wMockMessageService = new Mock<IMessageBoxUtil>();
            wMockMessageService.Setup(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()));

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMockMessageService.Object);
            wLibraControl.BorrowBook("test", It.IsAny<int>());

            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
            wMockMessageService.Verify(m => m.Show(MessageTypeEnum.DbError, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlで貸出処理中に予期せぬエラー発生テスト() {
            // BookRepositoryのモック作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Throws<Exception>();

            // メッセージボックスのモック作成
            var wMockMessageService = new Mock<IMessageBoxUtil>();
            wMockMessageService.Setup(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()));

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMockMessageService.Object);
            wLibraControl.BorrowBook("test", It.IsAny<int>());

            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
            wMockMessageService.Verify(m => m.Show(MessageTypeEnum.UnexpectedError, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlで書籍を返却するテスト() {
            // BookRepositoryのモック作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Returns(new Book {
                UserName = "test",
                BorrowingDate = "2023/01/01"
            });

            // メッセージボックスのモック作成
            var wMockMessageService = new Mock<IMessageBoxUtil>();
            wMockMessageService.Setup(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()));

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMockMessageService.Object);
            wLibraControl.ReturnBook(It.IsAny<int>());

            wMockRepository.Verify(m => m.CommitTransaction(), Times.Once);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Never);
            wMockMessageService.Verify(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()), Times.Never);
        }

        [Test]
        public void LibraControlで返却処理中に貸出中でないエラー発生テスト() {
            // BookRepositoryのモック作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Returns(new Book {
                UserName = null,
                BorrowingDate = null
            });

            // メッセージボックスのモック作成
            var wMockMessageService = new Mock<IMessageBoxUtil>();
            wMockMessageService.Setup(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()));

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMockMessageService.Object);
            wLibraControl.ReturnBook(It.IsAny<int>());

            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
            wMockMessageService.Verify(m => m.Show(MessageTypeEnum.NotBorrowed, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlで返却処理中にDBエラー発生テスト() {
            // BookRepositoryのモック作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Throws<SQLiteException>();

            // メッセージボックスのモック作成
            var wMockMessageService = new Mock<IMessageBoxUtil>();
            wMockMessageService.Setup(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()));

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMockMessageService.Object);
            wLibraControl.ReturnBook(It.IsAny<int>());

            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
            wMockMessageService.Verify(m => m.Show(MessageTypeEnum.DbError, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlで返却処理中に予期せぬエラー発生テスト() {
            // BookRepositoryのモック作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Throws<Exception>();

            // メッセージボックスのモック作成
            var wMockMessageService = new Mock<IMessageBoxUtil>();
            wMockMessageService.Setup(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()));

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMockMessageService.Object);
            wLibraControl.ReturnBook(It.IsAny<int>());

            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
            wMockMessageService.Verify(m => m.Show(MessageTypeEnum.UnexpectedError, It.IsAny<object>()), Times.Once);
        }
    }
}
