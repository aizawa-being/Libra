using Libra.Controls;
using Libra.Models;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraUnitTest {
    [TestFixture]
    public class OpenBdUnitTest {
        
        [TestCase("")]
        [TestCase("0000000000000")]
        [TestCase("9784065128442")]
        public async Task レスポンステスト(string vIsbn) {
            IOpenBdConnect openBdConnect = new OpenBdConnect();
            var response = await openBdConnect.SendRequest(vIsbn);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase("テスト書籍名1", "テスト著者名1", "テスト出版社1", "0123456789012", "テスト概要1")]
        [TestCase("", "", "", "", "")]
        public async Task 書籍情報設定成功テスト(string vTitle, string vAuthor, string vPublisher, string vBarcode, string vDescription) {
            // OpenBDConnectのMockを作成
            var openBdConnectMock = new Mock<IOpenBdConnect>();

            // レスポンスの設定
            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            response.Content = new MockHttpContent();

            // レスポンスのステータスコードを指定
            openBdConnectMock.Setup(o => o.SendRequest(It.IsAny<string>()))
                             .Returns(Task.FromResult(response));
            
            openBdConnectMock.Setup(o => o.PerseBookInfo(It.IsAny<string>()))
                             .Returns(new Book {
                                 Title = vTitle,
                                 Author = vAuthor,
                                 Publisher = vPublisher,
                                 Barcode = vBarcode,
                                 Description = vDescription
                             });
            
            // メッセージボックスのモックを作成
            var messageBoxMock = new Mock<IMessageBoxService>();

            messageBoxMock
                .Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>()))
                .Returns(DialogResult.OK);

            var addBookControl = new AddBookControl(openBdConnectMock.Object, messageBoxMock.Object);
            await addBookControl.SetAddBook(It.IsAny<string>());

            Assert.AreEqual(vTitle, addBookControl.BookToAdd.Title);
            Assert.AreEqual(vAuthor, addBookControl.BookToAdd.Author);
            Assert.AreEqual(vPublisher, addBookControl.BookToAdd.Publisher);
            Assert.AreEqual(vBarcode, addBookControl.BookToAdd.Barcode);
            Assert.AreEqual(vDescription, addBookControl.BookToAdd.Description);
        }

        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.InternalServerError)]
        [TestCase(HttpStatusCode.Ambiguous)]
        public void 書籍情報設定時ステータスコードエラーテスト(HttpStatusCode vHttpStatusCode) {
            // OpenBDConnectのMockを作成
            var openBdConnectMock = new Mock<IOpenBdConnect>();
            var responceMessage = new HttpResponseMessage(vHttpStatusCode);
            // レスポンスのステータスコードを指定
            openBdConnectMock.Setup(o => o.SendRequest(It.IsAny<string>()))
                             .Returns(Task.FromResult(responceMessage));

            // メッセージボックスのモックを作成
            var messageBoxMock = new Mock<IMessageBoxService>();

            messageBoxMock
                .Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>()))
                .Returns(DialogResult.OK);

            var addBookControl = new AddBookControl(openBdConnectMock.Object, messageBoxMock.Object);
            addBookControl.SetAddBook(It.IsAny<string>());

            messageBoxMock.Verify(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>()));
        }

        [TestCase("テスト書籍名1", "テスト著者名1", "テスト出版社1", "0123456789012", "テスト概要1")]
        [TestCase("", "", "", "", "")]
        public void 書籍情報抽出テスト(string vTitle, string vAuthor, string vPublisher, string vIsbn, string vDescription) {
            string jsonString = $@"[
                        {{
                            ""summary"": {{
                                ""title"": ""{vTitle}"",
                                ""author"": ""{vAuthor}"",
                                ""publisher"": ""{vPublisher}"",
                                ""isbn"": ""{vIsbn}"" }},
                            ""onix"": {{
                                ""CollateralDetail"": {{
                                    ""TextContent"": [ {{
                                        ""Text"": ""{vDescription}""
                                        }}
                                    ]
                                }}
                            }}
                        }}
                    ]";
            IOpenBdConnect openBdConnect = new OpenBdConnect();
            var book = openBdConnect.PerseBookInfo(jsonString);

            Assert.AreEqual(vTitle, book.Title);
            Assert.AreEqual(vAuthor, book.Author);
            Assert.AreEqual(vPublisher, book.Publisher);
            Assert.AreEqual(vIsbn, book.Barcode);
            Assert.AreEqual(vDescription, book.Description);
        }

        [TestCase("テスト書籍名1", "テスト著者名1", "テスト出版社1", "0123456789012")]
        [TestCase("", "", "", "")]
        public void 概要なし書籍情報抽出テスト(string vTitle, string vAuthor, string vPublisher, string vIsbn) {
            string jsonString = $@"[
                        {{
                            ""summary"": {{
                                ""title"": ""{vTitle}"",
                                ""author"": ""{vAuthor}"",
                                ""publisher"": ""{vPublisher}"",
                                ""isbn"": ""{vIsbn}""
                            }}
                        }}
                    ]";
            IOpenBdConnect openBdConnect = new OpenBdConnect();
            var book = openBdConnect.PerseBookInfo(jsonString);

            Assert.AreEqual(vTitle, book.Title);
            Assert.AreEqual(vAuthor, book.Author);
            Assert.AreEqual(vPublisher, book.Publisher);
            Assert.AreEqual(vIsbn, book.Barcode);
            Assert.AreEqual("", book.Description);
        }
        
        [Test]
        public void Null書籍情報抽出テスト() {
            string jsonString = "[\n  null\n]";
            IOpenBdConnect openBdConnect = new OpenBdConnect();
            var book = openBdConnect.PerseBookInfo(jsonString);

            Assert.AreEqual(null, book);
        }
    }
}
