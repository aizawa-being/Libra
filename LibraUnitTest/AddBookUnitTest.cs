using System.Data.Common;

using Moq;
using NUnit.Framework;

using Libra;
using System.Windows.Forms;
using System;

namespace LibraUnitTest {
    [TestFixture]
    public class AddBookUnitTest {
        
        [TestCase("追加書籍名", "追加著者名", "追加出版社", "追加概要", "0000000000000")]
        public void DBに書籍を追加するテスト(string vTitle, string vAuthor, string vPublisher, string vDescription, string vBarcode) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(true)) {
                // InMemoryDatabaseを利用
                IBookRepository wBookRepository = new BookRepository(wDbContext);
                var wBookService = new BookService(wBookRepository);
                var wAddBook = new Book{
                    Title = vTitle,
                    Author = vAuthor,
                    Publisher = vPublisher,
                    Description = vDescription,
                    Barcode = vBarcode
                };

                var wAddBookId = wBookService.AddBook(wAddBook);
                
                Assert.AreEqual(vTitle, wDbContext.Books.Find(wAddBookId).Title);
                Assert.AreEqual(vAuthor, wDbContext.Books.Find(wAddBookId).Author);
                Assert.AreEqual(vPublisher, wDbContext.Books.Find(wAddBookId).Publisher);
                Assert.AreEqual(vDescription, wDbContext.Books.Find(wAddBookId).Description);
                Assert.AreEqual(vBarcode, wDbContext.Books.Find(wAddBookId).Barcode);
            }
        }

        [Test]
        public void DBに書籍追加時に例外発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.AddBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save()).Throws<CustomDbException>();
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // BookServiceのモックを作成
            var wBookService = new BookService(wMockRepository.Object);

            Assert.Throws<CustomDbException>(() => wBookService.AddBook(new Book()));

            wMockRepository.Verify(m => m.AddBook(It.IsAny<Book>()), Times.Once);
            wMockRepository.Verify(m => m.Save(), Times.Once);
            wMockRepository.Verify(m => m.BeginTransaction(), Times.Once);
            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
        }

        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(2)]
        public void 取得済みの書籍をDBに追加するテスト(int vBookId) {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.AddBook(It.IsAny<Book>())).Callback<Book>(b => b.BookId = vBookId);
            wMockRepository.Setup(m => m.Save());
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();

            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>()))
                .Returns(DialogResult.OK);

            IOpenBdConnect wOpenBdConnect = new OpenBdConnect();
            var wAddBookControl = new AddBookControl(wOpenBdConnect, wMessageBoxMock.Object, wMockRepository.Object);
            
            var wBook = new Book {
                BookId = 1,
                Title = "追加書籍名",
                Author = "追加著者名",
                Publisher = "追加出版社",
                Description = "追加概要",
                Barcode = "0000000000000"
            };

            var wResult = wAddBookControl.TryRegisterAddBook(wBook, out int wBookId);

            wMockRepository.Verify(m => m.AddBook(It.IsAny<Book>()), Times.Once);
            wMockRepository.Verify(m => m.Save(), Times.Once);
            wMockRepository.Verify(m => m.BeginTransaction(), Times.Once);
            wMockRepository.Verify(m => m.CommitTransaction(), Times.Once);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Never);
            wMessageBoxMock.Verify(m => m.Show(It.IsAny<MessageTypeEnum>()), Times.Never);
            Assert.IsTrue(wResult);
            Assert.AreEqual(vBookId, wBookId);
        }

        [Test]
        public void 書籍情報がない時にDB追加を試みるテスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();
            
            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.AddBook(It.IsAny<Book>())).Callback<Book>(b => b.BookId = 1);
            wMockRepository.Setup(m => m.Save());
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();

            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>()))
                .Returns(DialogResult.OK);

            // コントローラのモックを作成
            IOpenBdConnect wOpenBdConnect = new OpenBdConnect();
            var wAddBookControl = new AddBookControl(wOpenBdConnect, wMessageBoxMock.Object, wMockRepository.Object);

            var wResult = wAddBookControl.TryRegisterAddBook(null, out int vBookId);
            
            Assert.IsFalse(wResult);
            Assert.AreEqual(-1, vBookId);
            wMockRepository.Verify(m => m.AddBook(It.IsAny<Book>()), Times.Never);
            wMockRepository.Verify(m => m.Save(), Times.Never);
            wMockRepository.Verify(m => m.BeginTransaction(), Times.Never);
            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Never);
            wMessageBoxMock.Verify(m => m.Show(It.IsAny<MessageTypeEnum>()), Times.Once);
        }

        [Test]
        public void DBアクセスでDBException発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.AddBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save()).Throws<CustomDbException>();
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();

            wMessageBoxMock.Setup(x => x.Show(It.IsAny<MessageTypeEnum>()))
                           .Returns(DialogResult.Cancel);

            // コントローラのモックを作成
            IOpenBdConnect wOpenBdConnect = new OpenBdConnect();
            var wAddBookControl = new AddBookControl(wOpenBdConnect, wMessageBoxMock.Object, wMockRepository.Object);

            var wResult = wAddBookControl.TryRegisterAddBook(new Book(), out int vBookId);
            
            Assert.IsFalse(wResult);
            Assert.AreEqual(-1, vBookId);

            wMockRepository.Verify(m => m.BeginTransaction(), Times.Once);
            wMockRepository.Verify(m => m.AddBook(It.IsAny<Book>()), Times.Once);
            wMockRepository.Verify(m => m.Save(), Times.Once);
            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);

            wMessageBoxMock.Verify(m => m.Show(MessageTypeEnum.DbError),
                                        Times.Once);
        }

        [Test]
        public void DBアクセスで予期せぬ例外発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.AddBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save()).Throws<Exception>();
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();

            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()))
                .Returns(DialogResult.OK);

            // コントローラのモックを作成
            IOpenBdConnect wOpenBdConnect = new OpenBdConnect();
            var wAddBookControl = new AddBookControl(wOpenBdConnect, wMessageBoxMock.Object, wMockRepository.Object);

            var wResult = wAddBookControl.TryRegisterAddBook(new Book(), out int vBookId);

            Assert.IsFalse(wResult);
            Assert.AreEqual(-1, vBookId);

            wMockRepository.Verify(m => m.BeginTransaction(), Times.Once);
            wMockRepository.Verify(m => m.AddBook(It.IsAny<Book>()), Times.Once);
            wMockRepository.Verify(m => m.Save(), Times.Once);
            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);

            wMessageBoxMock.Verify(m => m.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()), Times.Once);
        }

        /// <summary>
        /// DbExceptionの派生クラス
        /// </summary>
        public class CustomDbException : DbException {
            public CustomDbException() : base() {
                // コンストラクタの実装
            }
        }
    }
}
