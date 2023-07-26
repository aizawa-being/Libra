using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Common;

namespace Libra {
    /// <summary>
    /// 書籍追加用コントローラです。
    /// </summary>
    public class AddBookControl : IAddBookControl {
        /// <summary>
        /// 追加する書籍
        /// </summary>
        private Book FAddBook;
        private readonly IOpenBdConnect FOpenBdConnect;
        private readonly IMessageBoxService FMessageBoxService;
        private readonly IBookRepository FBookRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AddBookControl() {
            this.FOpenBdConnect = new OpenBdConnect();
            this.FMessageBoxService = new MessageBoxService();
            this.FBookRepository = new BookRepository(new BooksDbContext());
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vOpenBdConnect"></param>
        /// <param name="vMessageBoxService"></param>
        /// <param name="vBookRepository"></param>
        public AddBookControl(IOpenBdConnect vOpenBdConnect, IMessageBoxService vMessageBoxService, IBookRepository vBookRepository) {
            this.FOpenBdConnect = vOpenBdConnect;
            this.FMessageBoxService = vMessageBoxService;
            this.FBookRepository = vBookRepository;
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
                this.MessageBoxShow(ErrorMessageConst.C_NetworkError,
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
                    this.MessageBoxShow(ErrorMessageConst.C_BookNotFound,
                                        ErrorMessageConst.C_BookNotFoundCaption,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Asterisk);
                    this.FAddBook = null;
                    return;
                }
                this.FAddBook = wBook;
                return;

            } else if (wResponse.StatusCode >= HttpStatusCode.BadRequest && wResponse.StatusCode < HttpStatusCode.InternalServerError) {
                // 400番台クライアントエラー発生
                this.MessageBoxShow(ErrorMessageConst.C_ClientError,
                                    ErrorMessageConst.C_ClientErrorCaption,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                this.FAddBook = null;
                return;

            } else if (wResponse.StatusCode >= HttpStatusCode.InternalServerError) {
                // 500番台サーバーエラー発生
                this.MessageBoxShow(ErrorMessageConst.C_ServerError,
                                    ErrorMessageConst.C_ServerErrorCaption,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                this.FAddBook = null;
                return;

            } else {
                // 予期せぬエラー
                this.MessageBoxShow(string.Format(ErrorMessageConst.C_UnexpectedError, wResponse.StatusCode),
                                    ErrorMessageConst.C_UnexpectedErrorCaption,
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
        /// 取得した書籍情報をDBに登録します。
        /// 書籍追加成功時、追加した書籍のIDが通知されます。
        /// 書籍追加失敗時は、-1が渡されます。
        /// </summary>
        /// <returns>int </returns>
        public bool TryRegisterAddBook(Book vAddBook, out int vBookId) {
            if (vAddBook != null) {
                // データ追加前にデータベースが削除されている場合、テーブルを再構築する。
                // EF6の場合、キャッシュにテーブル情報が残っているとテーブルが自動作成されない。
                if (!this.FBookRepository.DatabaseExists()) {
                    this.FBookRepository.InitializeDatabase();
                }
                try {
                    using (var wBookService = new BookService(this.FBookRepository)) {
                        vBookId = wBookService.AddBook(vAddBook);

                        // 書籍追加成功
                        return true;
                    }
                } catch (DbException) {
                    // データベースエラーを表示
                    this.MessageBoxShow(ErrorMessageConst.C_DbError,
                                        ErrorMessageConst.C_DbErrorCaprion,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                } catch (Exception e) {
                    // 予期せぬエラー
                    this.MessageBoxShow(string.Format(ErrorMessageConst.C_UnexpectedError, e.Message),
                                        ErrorMessageConst.C_UnexpectedErrorCaprion,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                }
            } else {
                // 書籍情報未取得エラーを表示
                this.MessageBoxShow(ErrorMessageConst.C_BookInfoUnacquiredError,
                                    ErrorMessageConst.C_BookInfoUnacquiredErrorCaprion,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Asterisk);
            }
            // 書籍情報取得失敗
            vBookId = -1;
            return false;
        }

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <param name="vButton"></param>
        /// <param name="vIcon"></param>
        public DialogResult MessageBoxShow(string vMessage, string vCaption, MessageBoxButtons vButton, MessageBoxIcon vIcon) {
            return this.FMessageBoxService.Show(vMessage, vCaption, vButton, vIcon);
        }
    }
}
