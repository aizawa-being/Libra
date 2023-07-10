using Libra;
using Libra.Controls;
using Libra.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
        
        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public void レスポンスのステータスコードテスト(HttpStatusCode vHttpStatusCode) {
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
    }
}
