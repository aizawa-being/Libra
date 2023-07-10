using Libra.Models;
using System.Windows.Forms;

namespace Libra.Controls {
    public class AddBookControl {
        /// <summary>
        /// 追加する書籍情報
        /// </summary>
        public Book BookToAdd { get; private set; }
        
        /// <summary>
        /// ISBNコードと一致する書籍情報を設定します。
        /// </summary>
        /// <param name="vIsbn"></param>
        public async void SetAddBook(string vIsbn) {
            IOpenBdConnect openBdConnect = new OpenBdConnect();
            var strBook = await openBdConnect.GetBookByIsbn(vIsbn);
            if (strBook.StartsWith(ErrorMessageConst.NetworkError)) {
                // ネットワークエラー発生時
                MessageBox.Show(ErrorMessageConst.NetworkError, ErrorMessageConst.NetworkErrorCaption, MessageBoxButtons.OK);
                return;
            } else if (strBook.StartsWith(ErrorMessageConst.ServerError)) {
                // サーバーエラー発生時
                MessageBox.Show(ErrorMessageConst.ServerError, ErrorMessageConst.ServerErrorCaption, MessageBoxButtons.OK);
                return;
            } else if (strBook.StartsWith(ErrorMessageConst.UnexpectedError)) {
                // 予期せぬエラー発生時
                MessageBox.Show(ErrorMessageConst.UnexpectedError, ErrorMessageConst.UnexpectedErrorCaprion, MessageBoxButtons.OK);
                return;
            } else {
                // リクエスト成功時は文字列をJsonに変換し書籍情報を抽出する
                var book = openBdConnect.PerseBookInfo(strBook);
                if (book == null) {
                    MessageBox.Show(ErrorMessageConst.BookNotFound, ErrorMessageConst.BookNotFoundCaption, MessageBoxButtons.OK);
                    return;
                }
                this.BookToAdd = book;
                return;
            }
        }
    }
}
