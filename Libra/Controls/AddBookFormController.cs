using Libra.Models;
using Libra.Views;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra.Controls {
    public class AddBookFormController {
        /// <summary>
        /// 追加する書籍
        /// </summary>
        public Book BookToAdd { get; private set; }
        private readonly IOpenBdConnect F_OpenBdConnect;
        private readonly IMessageBoxService F_MessageBoxService;

        public AddBookFormController() {
            this.F_OpenBdConnect = new OpenBdConnect();
            this.F_MessageBoxService = new MessageBoxService();
        }

        public AddBookFormController(IOpenBdConnect vOpenBdConnect, IMessageBoxService vMessageBoxService) {
            this.F_OpenBdConnect = vOpenBdConnect;
            this.F_MessageBoxService = vMessageBoxService;
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public void ShowAddBookForm() {
            using (var addBookForm = new AddBookForm()) {
                addBookForm.ShowDialog();
            }
        }
        
        /// <summary>
        /// ISBNコードと一致する書籍情報を設定します。
        /// </summary>
        /// <param name="vIsbn"></param>
        public async Task<bool> SetAddBook(string vIsbn) {
            // リクエストを送信
            var response = await this.F_OpenBdConnect.SendRequest(vIsbn);
            if (response == null) {
                // HttpRequestException発生
                this.F_MessageBoxService.Show(ErrorMessageConst.NetworkError,
                                              ErrorMessageConst.NetworkErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                return false;
            }

            if (response.IsSuccessStatusCode) {
                // レスポンスの取得に成功
                var strBook = await response.Content.ReadAsStringAsync();
                // 文字列をJsonに変換し書籍情報を抽出する
                var book = this.F_OpenBdConnect.PerseBookInfo(strBook);
                if (book == null) {
                    this.F_MessageBoxService.Show(ErrorMessageConst.BookNotFound,
                                                  ErrorMessageConst.BookNotFoundCaption,
                                                  MessageBoxButtons.OK,
                                                  MessageBoxIcon.Asterisk);
                    return false;
                }
                this.BookToAdd = book;
                return true;

            } else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError) {
                // 400番台エラー発生
                this.F_MessageBoxService.Show(ErrorMessageConst.NetworkError,
                                              ErrorMessageConst.NetworkErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                return false;

            } else if (response.StatusCode >= HttpStatusCode.InternalServerError) {
                // 500番台エラー発生
                this.F_MessageBoxService.Show(ErrorMessageConst.ServerError,
                                              ErrorMessageConst.ServerErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                return false;

            } else {
                // 予期せぬエラー
                this.F_MessageBoxService.Show(string.Format(ErrorMessageConst.UnexpectedError, response.StatusCode),
                                              ErrorMessageConst.UnexpectedErrorCaprion,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
