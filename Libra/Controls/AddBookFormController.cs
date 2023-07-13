using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra {
    public class AddBookFormController : IAddBookController {
        /// <summary>
        /// 追加する書籍
        /// </summary>
        private Book FAddBook;
        private readonly IOpenBdConnect FOpenBdConnect;
        private readonly IMessageBoxService FMessageBoxService;

        public AddBookFormController() {
            this.FOpenBdConnect = new OpenBdConnect();
            this.FMessageBoxService = new MessageBoxService();
        }

        public AddBookFormController(IOpenBdConnect vOpenBdConnect, IMessageBoxService vMessageBoxService) {
            this.FOpenBdConnect = vOpenBdConnect;
            this.FMessageBoxService = vMessageBoxService;
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public int ShowAddBookForm() {
            using (var wAddBookForm = new AddBookForm()) {
                wAddBookForm.ShowDialog();
                return wAddBookForm.AddBookId;
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
            var wResponse = await this.FOpenBdConnect.SendRequest(vIsbn);
            if (wResponse == null) {
                // HttpRequestException発生
                this.FMessageBoxService.Show(ErrorMessageConst.C_NetworkError,
                                              ErrorMessageConst.C_NetworkErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                this.FAddBook = null;
                return;
            }

            if (wResponse.IsSuccessStatusCode) {
                // レスポンスの取得に成功
                var wStrBook = await wResponse.Content.ReadAsStringAsync();
                // 文字列をJsonに変換し書籍情報を抽出する
                var wBook = this.FOpenBdConnect.PerseBookInfo(wStrBook);
                if (wBook == null) {
                    this.FMessageBoxService.Show(ErrorMessageConst.C_BookNotFound,
                                                  ErrorMessageConst.C_BookNotFoundCaption,
                                                  MessageBoxButtons.OK,
                                                  MessageBoxIcon.Asterisk);
                    this.FAddBook = null;
                    return;
                }
                this.FAddBook = wBook;
                return;

            } else if (wResponse.StatusCode >= HttpStatusCode.BadRequest && wResponse.StatusCode < HttpStatusCode.InternalServerError) {
                // 400番台エラー発生
                this.FMessageBoxService.Show(ErrorMessageConst.C_ClientError,
                                              ErrorMessageConst.C_ClientErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                this.FAddBook = null;
                return;

            } else if (wResponse.StatusCode >= HttpStatusCode.InternalServerError) {
                // 500番台エラー発生
                this.FMessageBoxService.Show(ErrorMessageConst.C_ServerError,
                                              ErrorMessageConst.C_ServerErrorCaption,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                this.FAddBook = null;
                return;

            } else {
                // 予期せぬエラー
                this.FMessageBoxService.Show(string.Format(ErrorMessageConst.C_UnexpectedError, wResponse.StatusCode),
                                              ErrorMessageConst.C_UnexpectedErrorCaprion,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                this.FAddBook = null;
                return;
            }
        }

        /// <summary>
        /// 追加用書籍情報の取得
        /// </summary>
        /// <returns></returns>
        public Book GetAddBook() {
            return this.FAddBook;
        }

        /// <summary>
        /// 追加用書籍情報が存在するか
        /// </summary>
        /// <returns>true : 存在する
        ///         false : null</returns>
        public bool ExistAddBook() {
            return this.FAddBook != null;
        }

        /// <summary>
        /// 書籍情報をDBに登録します。
        /// </summary>
        /// <returns></returns>
        public int RegisterAddBook() {
            var wBookService = new BookService();
            return wBookService.AddBook(this.FAddBook);
        }
    }
}
