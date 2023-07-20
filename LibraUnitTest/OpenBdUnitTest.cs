using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

using Moq;
using NUnit.Framework;

using Libra;

namespace LibraUnitTest {
    [TestFixture]
    public class OpenBdUnitTest {

        [TestCase("")]
        [TestCase("999999999999")]
        [TestCase("0000000000000")]
        [TestCase("9999999999999")]
        [TestCase("00000000000000")]
        public async Task レスポンステスト(string vIsbn) {
            IOpenBdConnect wOpenBdConnect = new OpenBdConnect();
            var wResponse = await wOpenBdConnect.SendRequest(vIsbn);

            Assert.AreEqual(HttpStatusCode.OK, wResponse.StatusCode);
        }

        [TestCase("テスト書籍名1", "テスト著者名1", "テスト出版社1", "0123456789012", "テスト概要1")]
        [TestCase("", "", "", "", "")]
        public async Task 書籍情報設定成功テスト(string vTitle, string vAuthor, string vPublisher, string vBarcode, string vDescription) {
            // OpenBDConnectのMockを作成
            var wOpenBdConnectMock = new Mock<IOpenBdConnect>();

            // レスポンスの設定
            var wResponse = new HttpResponseMessage(HttpStatusCode.Accepted);
            wResponse.Content = new MockHttpContent("");

            // レスポンスのステータスコードを指定
            wOpenBdConnectMock.Setup(o => o.SendRequest(It.IsAny<string>()))
                             .Returns(Task.FromResult(wResponse));

            wOpenBdConnectMock.Setup(o => o.PerseBookInfo(It.IsAny<string>()))
                             .Returns(new Book {
                                 Title = vTitle,
                                 Author = vAuthor,
                                 Publisher = vPublisher,
                                 Barcode = vBarcode,
                                 Description = vDescription
                             });

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();

            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            
            IAddBookControl wAddBookControl = new AddBookControl(wOpenBdConnectMock.Object, wMessageBoxMock.Object);
            await wAddBookControl.SetAddBook(It.IsAny<string>());
            var wBook = wAddBookControl.GetAddBook();

            Assert.AreEqual(vTitle, wBook.Title);
            Assert.AreEqual(vAuthor, wBook.Author);
            Assert.AreEqual(vPublisher, wBook.Publisher);
            Assert.AreEqual(vBarcode, wBook.Barcode);
            Assert.AreEqual(vDescription, wBook.Description);
        }

        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.InternalServerError)]
        [TestCase(HttpStatusCode.Ambiguous)]
        public void 書籍情報設定時ステータスコードエラーテスト(HttpStatusCode vHttpStatusCode) {
            // OpenBDConnectのMockを作成
            var wOpenBdConnectMock = new Mock<IOpenBdConnect>();
            var wResponceMessage = new HttpResponseMessage(vHttpStatusCode);
            // レスポンスのステータスコードを指定
            wOpenBdConnectMock.Setup(o => o.SendRequest(It.IsAny<string>()))
                             .Returns(Task.FromResult(wResponceMessage));

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();

            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            
            IAddBookControl wAddBookControl = new AddBookControl(wOpenBdConnectMock.Object, wMessageBoxMock.Object);
            wAddBookControl.SetAddBook(It.IsAny<string>());

            wMessageBoxMock.Verify(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()));
            Assert.IsNull(wAddBookControl.GetAddBook());
        }

        [Test]
        public void レスポンスがnullの場合のテスト() {
            // OpenBDConnectのMockを作成
            var wOpenBdConnectMock = new Mock<IOpenBdConnect>();
            HttpResponseMessage wResponceMessage = null;

            // レスポンスのステータスコードを指定
            wOpenBdConnectMock.Setup(o => o.SendRequest(It.IsAny<string>()))
                             .Returns(Task.FromResult(wResponceMessage));

            // メッセージボックスのモックを作成
            var wMessageBoxMock = new Mock<IMessageBoxService>();

            wMessageBoxMock
                .Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()))
                .Returns(DialogResult.OK);
            
            IAddBookControl wAddBookControl = new AddBookControl(wOpenBdConnectMock.Object, wMessageBoxMock.Object);
            wAddBookControl.SetAddBook(It.IsAny<string>());

            wMessageBoxMock.Verify(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>(), It.IsAny<MessageBoxIcon>()));
            Assert.IsNull(wAddBookControl.GetAddBook());
        }

        [Test]
        public async Task HttpRequestException発生テスト() {
            var wHttpClientMock = new Mock<IHttpClient>();
            wHttpClientMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                          .ThrowsAsync(new HttpRequestException());

            var wOpenBdConnect = new OpenBdConnect(wHttpClientMock.Object);

            var wResult = await wOpenBdConnect.SendRequest(It.IsAny<string>());

            Assert.IsNull(wResult);
        }

        [TestCase("テスト書籍名1", "テスト著者名1", "テスト出版社1", "0123456789012", "テスト概要1")]
        [TestCase("", "", "", "", "")]
        public void 書籍情報抽出テスト(string vTitle, string vAuthor, string vPublisher, string vIsbn, string vDescription) {
            string wJsonString = $@"[
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
            IOpenBdConnect wOpenBdConnect = new OpenBdConnect();
            var wBook = wOpenBdConnect.PerseBookInfo(wJsonString);

            Assert.AreEqual(vTitle, wBook.Title);
            Assert.AreEqual(vAuthor, wBook.Author);
            Assert.AreEqual(vPublisher, wBook.Publisher);
            Assert.AreEqual(vIsbn, wBook.Barcode);
            Assert.AreEqual(vDescription, wBook.Description);
        }

        [TestCase("テスト書籍名1", "テスト著者名1", "テスト出版社1", "0123456789012")]
        [TestCase("", "", "", "")]
        public void 概要なし書籍情報抽出テスト(string vTitle, string vAuthor, string vPublisher, string vIsbn) {
            string wJsonString = $@"[
                        {{
                            ""summary"": {{
                                ""title"": ""{vTitle}"",
                                ""author"": ""{vAuthor}"",
                                ""publisher"": ""{vPublisher}"",
                                ""isbn"": ""{vIsbn}""
                            }}
                        }}
                    ]";
            IOpenBdConnect wOpenBdConnect = new OpenBdConnect();
            var wBook = wOpenBdConnect.PerseBookInfo(wJsonString);

            Assert.AreEqual(vTitle, wBook.Title);
            Assert.AreEqual(vAuthor, wBook.Author);
            Assert.AreEqual(vPublisher, wBook.Publisher);
            Assert.AreEqual(vIsbn, wBook.Barcode);
            Assert.AreEqual("", wBook.Description);
        }

        [Test]
        public void ISBNに一致する書籍がない場合のテスト() {
            string wJsonString = "[\n  null\n]";
            IOpenBdConnect wOpenBdConnect = new OpenBdConnect();
            var wBook = wOpenBdConnect.PerseBookInfo(wJsonString);

            Assert.IsNull(wBook);
        }
    }
}