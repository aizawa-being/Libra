using Libra.Models;
using Libra.Views;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra.Controls {
    public class AddBookFormController : IAddBookController {
        /// <summary>
        /// 追加する書籍
        /// </summary>
        private Book F_AddBook;
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
        /// <returns>true  : 成功
        ///          false : 失敗</returns>
        public async Task SetAddBook(string vIsbn) {
            // リクエストを送信
            var response = await this.F_OpenBdConnect.SendRequest(vIsbn);
            if (response == null) {
                // HttpRequestException発生
                this.F_MessageBoxService.Show(ErrorMessageConst.NetworkError,
                                              ErrorMessageConst.NetworkErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                this.F_AddBook = null;
                return;
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
                    this.F_AddBook = null;
                    return;
                }
                this.F_AddBook = book;
                return;

            } else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError) {
                // 400番台エラー発生
                this.F_MessageBoxService.Show(ErrorMessageConst.ClientError,
                                              ErrorMessageConst.ClientErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                this.F_AddBook = null;
                return;

            } else if (response.StatusCode >= HttpStatusCode.InternalServerError) {
                // 500番台エラー発生
                this.F_MessageBoxService.Show(ErrorMessageConst.ServerError,
                                              ErrorMessageConst.ServerErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                this.F_AddBook = null;
                return;

            } else {
                // 予期せぬエラー
                this.F_MessageBoxService.Show(string.Format(ErrorMessageConst.UnexpectedError, response.StatusCode),
                                              ErrorMessageConst.UnexpectedErrorCaprion,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                this.F_AddBook = null;
                return;
            }
        }

        /// <summary>
        /// 追加用書籍情報の取得
        /// </summary>
        /// <returns></returns>
        public Book GetAddBook() {
            return this.F_AddBook;
        }

        /// <summary>
        /// 追加用書籍情報が存在するか
        /// </summary>
        /// <returns>true : 存在する
        ///         false : null</returns>
        public bool ExistAddBook() {
            return this.F_AddBook != null;
        }

        /// <summary>
        /// 書籍情報をDBに登録します。
        /// </summary>
        /// <returns></returns>
        public int RegisterAddBook() {
            var bookService = new BookService();
            return bookService.AddBook(this.F_AddBook);
        }
    }
}
