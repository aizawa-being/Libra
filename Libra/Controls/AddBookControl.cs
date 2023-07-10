using Libra.Models;
using System.Net;
using System.Windows.Forms;

namespace Libra.Controls {
    public class AddBookControl {
        /// <summary>
        /// 追加する書籍情報
        /// </summary>
        public Book BookToAdd { get; private set; }
        private readonly IOpenBdConnect F_OpenBdConnect;
        private readonly IMessageBoxService F_MessageBoxService;

        public AddBookControl() {
            this.F_OpenBdConnect = new OpenBdConnect();
            this.F_MessageBoxService = new MessageBoxService();
        }

        public AddBookControl(IOpenBdConnect vOpenBdConnect, IMessageBoxService vMessageBoxService) {
            this.F_OpenBdConnect = vOpenBdConnect;
            this.F_MessageBoxService = vMessageBoxService;
        }

        /// <summary>
        /// ISBNコードと一致する書籍情報を設定します。
        /// </summary>
        /// <param name="vIsbn"></param>
        public async void SetAddBook(string vIsbn) {
            var response = await this.F_OpenBdConnect.SendRequest(vIsbn);
            
            if (response.IsSuccessStatusCode) {
                // レスポンスの取得に成功
                var strBook = await response.Content.ReadAsStringAsync();
                // 文字列をJsonに変換し書籍情報を抽出する
                var book = this.F_OpenBdConnect.PerseBookInfo(strBook);
                if (book == null) {
                    this.F_MessageBoxService.Show(ErrorMessageConst.BookNotFound, ErrorMessageConst.BookNotFoundCaption, MessageBoxButtons.OK);
                    return;
                }
                this.BookToAdd = book;
                return;

            } else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError) {
                // 400番台エラー発生
                this.F_MessageBoxService.Show(ErrorMessageConst.NetworkError, ErrorMessageConst.NetworkErrorCaption, MessageBoxButtons.OK);
                return;

            } else if (response.StatusCode >= HttpStatusCode.InternalServerError) {
                // 500番台エラー発生
                this.F_MessageBoxService.Show(ErrorMessageConst.ServerError, ErrorMessageConst.ServerErrorCaption, MessageBoxButtons.OK);
                return;

            } else {
                // 予期せぬエラー
                this.F_MessageBoxService.Show(string.Format(ErrorMessageConst.UnexpectedError, response.StatusCode), ErrorMessageConst.UnexpectedErrorCaprion, MessageBoxButtons.OK);
                return;
            }
        }
    }
}
