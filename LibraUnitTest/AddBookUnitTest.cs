using Moq;
using NUnit.Framework;

using Libra;
using System;
using System.Reflection;
using Moq.Protected;
using System.Windows.Forms;

namespace LibraUnitTest {
    [TestFixture]
    public class AddBookUnitTest {
        
        [TestCase("追加書籍名", "追加著者名", "追加出版社", "追加概要", "0000000000000")]
        public void DBに書籍を追加するテスト(string vTitle, string vAuthor, string vPublisher, string vDescription, string vBarcode) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb()) {
                // ブックリポジトリのモックを作成
                IBookRepository wBookRepository = new BooksRepository(wDbContext);
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

        [TestCase(1)]
        [TestCase(2)]
        public void 取得済みの書籍をDBに追加するテスト(int vBookId) {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();
            var wBook = new Book {
                BookId = 1,
                Title = "追加書籍名",
                Author = "追加著者名",
                Publisher = "追加出版社",
                Description = "追加概要",
                Barcode = "0000000000000"
            };

            wMockRepository.Setup(m => m.AddBook(It.IsAny<Book>())).Callback<Book>(b => b.BookId = vBookId);
            wMockRepository.Setup(m => m.Save());

            var wAddBookFormController = new AddBookFormController(wMockRepository.Object);
            
            var wResult = wAddBookFormController.RegisterAddBook(wBook);

            Assert.AreEqual(vBookId, wResult);
        }

        [Test]
        public void 書籍情報がない時にDB追加を試みるテスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();
            
            wMockRepository.Setup(m => m.AddBook(It.IsAny<Book>())).Callback<Book>(b => b.BookId = 1);
            wMockRepository.Setup(m => m.Save());

            // コントローラのモックを作成
            var wAddBookFormController = new AddBookFormController(wMockRepository.Object);

            var wResult = wAddBookFormController.RegisterAddBook(null);
            
            Assert.AreEqual(-1, wResult);
            wMockRepository.Verify(m => m.AddBook(It.IsAny<Book>()), Times.Never);
            wMockRepository.Verify(m => m.Save(), Times.Never);
        }
        
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void 追加ボタンクリックイベントテスト(int vBookId) {
            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();

            // MessageBoxShowCalledが呼び出されたか判定するフラグ
            bool wIsMessageBoxCalled = false;

            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Callback(() => wIsMessageBoxCalled = true);

            // ブックリポジトリのモックを作成
            var wBookRepositoryMock = new Mock<IBookRepository>();

            wBookRepositoryMock.Setup(m => m.AddBook(It.IsAny<Book>())).Callback<Book>(b => b.BookId = vBookId);
            wBookRepositoryMock.Setup(m => m.Save());
            
            // コントローラのモックを作成
            var wAddControl = new AddBookFormController(wMessageBoxMock.Object, wBookRepositoryMock.Object);

            // privateフィールドのFAddBookを取得
            var wFAddBook = wAddControl.GetType().GetField("FAddBook", BindingFlags.NonPublic | BindingFlags.Instance);

            // FAddBookに値を設定
            wFAddBook.SetValue(wAddControl, new Book {
                Title = "",
                Author = "",
                Publisher = "",
                Description = "",
                Barcode = ""
            });

            // フォームのモックを作成
            var wAddForm = new AddBookForm(wAddControl);

            // リフレクションを使用してAddButtonClickメソッドを取得
            var wAddButtonClickMethod = wAddForm.GetType().GetMethod("AddButtonClick", BindingFlags.NonPublic | BindingFlags.Instance);
            
            // AddButtonClickメソッドを呼び出す
            wAddButtonClickMethod.Invoke(wAddForm, new object[] { null, EventArgs.Empty });
            
            // 書籍IDが-1のときのみ、メッセージボックスが表示される。
            if (vBookId == -1) {
                Assert.IsTrue(wIsMessageBoxCalled);
            } else {
                Assert.IsFalse(wIsMessageBoxCalled);
            }
        }
    }
}
