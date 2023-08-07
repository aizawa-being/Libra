using Libra;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace LibraUnitTest {
    [TestFixture]
    public class SearchBooksTest {
        private readonly Dictionary<int, Book> Books = new Dictionary<int, Book>() {
            {
                1, new Book {
                    BookId = 1,
                    Title = "テストタイトル1",
                    Author = "テスト著者1",
                    Publisher = "テスト出版社1",
                    Description = "テスト概要1",
                    UserName = "テスト利用者名1",
                    Barcode = "0000000000001",
                    IsDeleted = 0 }
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
                    IsDeleted = 0 }
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
                    IsDeleted = 0 }
            }
        };

        [TestCase("テスト")]
        [TestCase("タイトル")]
        public void BookServiceの全件ヒットする検索テスト(string vSearchWord) {
            // 検索ワードを入力
            IEnumerable<string> wSingleWords = new List<string>() { vSearchWord };

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);
                var wBooks = wBookService.SearchBooks(wSingleWords);

                Assert.AreEqual(this.Books.Count, wBooks.Count());

                int wCount = 0;
                foreach (var wBook in wBooks) {
                    wCount++;
                    Assert.AreEqual(this.Books[wCount], wBook);
                }
            }
        }

        [TestCase("タイトル1")]
        [TestCase("1")]
        public void BookServiceの1件のみヒットする検索テスト(string vSearchWord) {
            // 検索ワードを入力
            IEnumerable<string> wSingleWords = new List<string>() { vSearchWord };

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);
                var wBooks = wBookService.SearchBooks(wSingleWords);

                Assert.AreEqual(1, wBooks.Count());
                Assert.AreEqual(this.Books[1], wBooks.FirstOrDefault());
            }
        }

        [TestCase("4")]
        [TestCase("ヒットしません")]
        public void BookServiceの検索ワードでヒットしないテスト(string vSearchWord) {
            // 検索ワードを入力
            IEnumerable<string> wSingleWords = new List<string>() { vSearchWord };

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);
                var wBooks = wBookService.SearchBooks(wSingleWords);

                Assert.AreEqual(0, wBooks.Count());
            }
        }

        [TestCase("テ", "スト")]
        [TestCase("テス", "ト")]
        public void BookServiceの複数ワードで全件ヒットする検索テスト(string vSearchWord1, string vSearchWord2) {
            // 検索ワードを入力
            IEnumerable<string> wSingleWords = new List<string>() { vSearchWord1, vSearchWord2 };

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);
                var wBooks = wBookService.SearchBooks(wSingleWords);

                Assert.AreEqual(this.Books.Count, wBooks.Count());

                int wCount = 0;
                foreach (var wBook in wBooks) {
                    wCount++;
                    Assert.AreEqual(this.Books[wCount], wBook);
                }
            }
        }

        [TestCase("テスト", "タイトル1")]
        [TestCase("テスト", "1")]
        public void BookServiceの複数ワードで1件のみヒットする検索テスト(string vSearchWord1, string vSearchWord2) {
                // 検索ワードを入力
                IEnumerable<string> wSingleWords = new List<string>() { vSearchWord1, vSearchWord2 };

                var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);
                var wBooks = wBookService.SearchBooks(wSingleWords);

                Assert.AreEqual(1, wBooks.Count());
                Assert.AreEqual(this.Books[1], wBooks.FirstOrDefault());
            }
        }

        [TestCase("テスト", "ヒットしません")]
        [TestCase("1", "ヒットしません")]
        [TestCase("ヒット", "しません")]
        public void BookServiceの複数ワードで1件もヒットしないテスト(string vSearchWord1, string vSearchWord2) {
            // 検索ワードを入力
            IEnumerable<string> wSingleWords = new List<string>() { vSearchWord1, vSearchWord2 };

            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                ILibraBookService wBookService = new BookService(() => wBookRepository);
                var wBooks = wBookService.SearchBooks(wSingleWords);

                Assert.AreEqual(0, wBooks.Count());
            }
        }

        [TestCase("テストタイトル4")]
        [TestCase("テスト著者4")]
        [TestCase("テスト出版社4")]
        [TestCase("テスト概要4")]
        [TestCase("テスト利用者名4")]
        [TestCase("テスト タイトル 4")]
        [TestCase("テスト 著者 4")]
        [TestCase("テスト 出版社 4")]
        [TestCase("テスト 概要 4")]
        [TestCase("テスト 利用者名 4")]
        public void LibraControlの1単語検索時0件該当テスト(string vSearchWord) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);

                var wMockMessageService = new Mock<MessageBoxService>();
                ILibraControl wLibraControl = new LibraControl(new BooksTable(), () => wBookRepository, wMockMessageService.Object);

                wLibraControl.SearchBooks(vSearchWord);
                Assert.AreEqual(0, wLibraControl.GetBooksDataTable().Count);
            }
        }

        [TestCase("テストタイトル1")]
        [TestCase("テスト著者1")]
        [TestCase("テスト出版社1")]
        [TestCase("テスト概要1")]
        [TestCase("テスト利用者名1")]
        [TestCase("テスト 1")]
        [TestCase("テスト タイトル 1")]
        [TestCase("テスト 著者 1")]
        [TestCase("テスト 出版社 1")]
        [TestCase("テスト 概要 1")]
        [TestCase("テスト 利用者名 1")]
        public void LibraControlの検索時1件該当テスト(string vSearchWord) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);

                var wMockMessageService = new Mock<MessageBoxService>();
                ILibraControl wLibraControl = new LibraControl(new BooksTable(), () => wBookRepository, wMockMessageService.Object);

                wLibraControl.SearchBooks(vSearchWord);
                Assert.AreEqual(1, wLibraControl.GetBooksDataTable().Count);
                Assert.AreEqual("テストタイトル1", wLibraControl.GetBooksDataTable().ElementAt(0).Title);
            }
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("テスト")]
        [TestCase("テ スト")]
        [TestCase("テ ス ト")]
        public void LibraControlの検索時全件該当テスト(string vSearchWord) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(false)) {
                // InMemoryDatabaseを利用
                // 初期データの用意
                wDbContext.Books.Add(Books[1]);
                wDbContext.Books.Add(Books[2]);
                wDbContext.Books.Add(Books[3]);
                wDbContext.SaveChanges();

                IBookRepository wBookRepository = new BookRepository(wDbContext);
                
                var wMockMessageService = new Mock<MessageBoxService>();
                ILibraControl wLibraControl = new LibraControl(new BooksTable(), () => wBookRepository, wMockMessageService.Object);

                wLibraControl.SearchBooks(vSearchWord);
                Assert.AreEqual(3, wLibraControl.GetBooksDataTable().Count);
                Assert.AreEqual("テストタイトル1", wLibraControl.GetBooksDataTable().ElementAt(0).Title);
                Assert.AreEqual("テストタイトル2", wLibraControl.GetBooksDataTable().ElementAt(1).Title);
                Assert.AreEqual("テストタイトル3", wLibraControl.GetBooksDataTable().ElementAt(2).Title);
            }
        }

        [Test]
        public void LibraControlの検索時にDbException発生テスト() {
            // リポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBooks()).Throws(new SQLiteException());

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();
            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()))
                .Returns(DialogResult.OK);

            // LibraControlのインスタンスにモックを注入
            var wMockTable = new Mock<BooksTable>();
            ILibraControl wLibraControl = new LibraControl(wMockTable.Object, () => wMockRepository.Object, wMessageBoxMock.Object);
            wLibraControl.SearchBooks(string.Empty);

            // 例外発生時、メッセージボックスが表示されていることを確認
            wMessageBoxMock.Verify(m => m.Show(MessageTypeEnum.DbError, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlの検索時にEntityException発生テスト() {
            // リポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBooks()).Throws(new EntityException());
            
            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();
            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()))
                .Returns(DialogResult.OK);

            // LibraControlのインスタンスにモックを注入
            var wMockTable = new Mock<BooksTable>();
            ILibraControl wLibraControl = new LibraControl(wMockTable.Object, () => wMockRepository.Object, wMessageBoxMock.Object);
            wLibraControl.SearchBooks(string.Empty);

            // 例外発生時、メッセージボックスが表示されていることを確認
            wMessageBoxMock.Verify(m => m.Show(MessageTypeEnum.DbError, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlの検索時にException発生テスト() {
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBooks()).Throws(new Exception());

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();
            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()))
                .Returns(DialogResult.OK);

            var wMockTable = new Mock<BooksTable>();

            ILibraControl wLibraControl = new LibraControl(wMockTable.Object, () => wMockRepository.Object, wMessageBoxMock.Object);
            wLibraControl.SearchBooks(string.Empty);

            // 例外発生時、メッセージボックスが表示されていることを確認
            wMessageBoxMock.Verify(m => m.Show(MessageTypeEnum.UnexpectedError, It.IsAny<object>()), Times.Once);
        }
    }
}
