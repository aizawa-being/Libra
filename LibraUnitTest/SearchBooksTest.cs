using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Moq;
using NUnit.Framework;
using System.Data.Entity.Core;
using System.Data.SQLite;

using Libra;

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

                var wMockMessageService = new Mock<IMessageBoxUtil>();
                ILibraControl wLibraControl = new LibraControl(() => wBookRepository, wMockMessageService.Object);

                var wBooks = wLibraControl.SearchBooks(vSearchWord);
                Assert.AreEqual(0, wBooks.Count());
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

                var wMockMessageService = new Mock<IMessageBoxUtil>();
                ILibraControl wLibraControl = new LibraControl(() => wBookRepository, wMockMessageService.Object);

                var wBooks = wLibraControl.SearchBooks(vSearchWord);
                Assert.AreEqual(1, wBooks.Count());
                Assert.AreEqual("テストタイトル1", wBooks.ElementAt(0).Title);
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
                
                var wMockMessageService = new Mock<IMessageBoxUtil>();
                ILibraControl wLibraControl = new LibraControl(() => wBookRepository, wMockMessageService.Object);

                var wBooks = wLibraControl.SearchBooks(vSearchWord);
                Assert.AreEqual(3, wBooks.Count());
                Assert.AreEqual("テストタイトル1", wBooks.ElementAt(0).Title);
                Assert.AreEqual("テストタイトル2", wBooks.ElementAt(1).Title);
                Assert.AreEqual("テストタイトル3", wBooks.ElementAt(2).Title);
            }
        }

        [Test]
        public void LibraControlの検索時にDbException発生テスト() {
            // リポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBooks()).Throws(new SQLiteException());

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxUtil>();
            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()))
                .Returns(DialogResult.OK);

            // LibraControlのインスタンスにモックを注入
            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMessageBoxMock.Object);
            var wBooks = wLibraControl.SearchBooks(string.Empty);

            // 例外発生時、メッセージボックスが表示されていることを確認
            wMessageBoxMock.Verify(m => m.Show(MessageTypeEnum.DbError, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlの検索時にEntityException発生テスト() {
            // リポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBooks()).Throws(new EntityException());
            
            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxUtil>();
            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()))
                .Returns(DialogResult.OK);

            // LibraControlのインスタンスにモックを注入
            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMessageBoxMock.Object);
            var wBooks = wLibraControl.SearchBooks(string.Empty);

            // 例外発生時、メッセージボックスが表示されていることを確認
            wMessageBoxMock.Verify(m => m.Show(MessageTypeEnum.DbError, It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void LibraControlの検索時にException発生テスト() {
            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBooks()).Throws(new Exception());

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxUtil>();
            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<MessageTypeEnum>(), It.IsAny<object>()))
                .Returns(DialogResult.OK);

            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMessageBoxMock.Object);
            var wBooks = wLibraControl.SearchBooks(string.Empty);

            // 例外発生時、メッセージボックスが表示されていることを確認
            wMessageBoxMock.Verify(m => m.Show(MessageTypeEnum.UnexpectedError, It.IsAny<object>()), Times.Once);
        }

        [TestCase("書籍名", 3, TestName = "検索時にTitleが一致する3件取得できること")]
        [TestCase("著者名", 3, TestName = "検索時にAuthorが一致する3件取得できること")]
        [TestCase("出版社", 3, TestName = "検索時にPublisherが一致する3件取得できること")]
        [TestCase("概要", 2, TestName = "検索時にDescriptionが一致する2件取得できること")]
        [TestCase("利用者名", 1, TestName = "検索時にUserNameが一致する1件取得できること")]
        [TestCase("該当なし", 0, TestName = "検索時に一致する書籍がないこと")]
        public void 削除フラグが立っている書籍が検索されないこと(string vSearchWord, int vResult) {

            IEnumerable<Book> wBookList = new List<Book>{
                new Book {
                    Title = "書籍名1",
                    Author = "著者名1",
                    Publisher = "出版社1",
                    Description = "概要1",
                    UserName = "利用者名1",
                    IsDeleted = 0
                },
                new Book {
                    Title = "書籍名2",
                    Author = "著者名2",
                    Publisher = "出版社2",
                    Description = "概要2",
                    UserName = null,
                    IsDeleted = 0
                },
                new Book {
                    Title = "書籍名3",
                    Author = "著者名3",
                    Publisher = "出版社3",
                    Description = null,
                    UserName = null,
                    IsDeleted = 0
                },
                new Book {
                    Title = "書籍名4",
                    Author = "著者名4",
                    Publisher = "出版社4",
                    Description = "概要4",
                    UserName = null,
                    IsDeleted = 1
                },
                new Book {
                    Title = "書籍名5",
                    Author = "著者名5",
                    Publisher = "出版社5",
                    Description = null,
                    UserName = null,
                    IsDeleted = 1
                }
            };

            var wMockRepository = new Mock<IBookRepository>();
            wMockRepository.Setup(m => m.GetBooks()).Returns(wBookList);

            var wMessageBoxMock = new Mock<IMessageBoxUtil>();
            ILibraControl wLibraControl = new LibraControl(() => wMockRepository.Object, wMessageBoxMock.Object);

            var wBooks = wLibraControl.SearchBooks(vSearchWord);

            Assert.AreEqual(vResult, wBooks.Count());
        }
    }
}
