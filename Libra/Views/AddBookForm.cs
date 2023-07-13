using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Libra {
    public partial class AddBookForm : Form {
        /// <summary>
        /// 追加した書籍のID
        /// 書籍を追加していない場合の初期値は-1
        /// </summary>
        public int AddBookId { get; private set; }
        private readonly IAddBookController FAddBookController;

        public AddBookForm() {
            this.FAddBookController = new AddBookFormController();
            InitializeComponent();
        }

        public AddBookForm(IAddBookController vAddBookController) {
            this.FAddBookController = vAddBookController;
            InitializeComponent();
        }

        private async void GetBookInfoButtonClickAsync(object sender, EventArgs e) {
            await this.FAddBookController.SetAddBook(this.isbnTextBox.Text);
            if (this.FAddBookController.ExistAddBook()) {
                // 書籍情報取得成功時
                var wBook = this.FAddBookController.GetAddBook();
                this.titleLabel.Text = wBook.Title;
                this.authorLabel.Text = wBook.Author;
            } else {
                // 書籍情報取得失敗時
                this.titleLabel.Text = "";
                this.authorLabel.Text = "";
            }
        }

        private void AddButtonClick(object sender, EventArgs e) {
            this.AddBookId = this.FAddBookController.RegisterAddBook(this.FAddBookController.GetAddBook());
            if (this.AddBookId == -1) {
                // 書籍情報が未取得の場合、書籍追加は行わない。
                this.FAddBookController.MessageBoxShow(ErrorMessageConst.C_BookInfoUnacquiredError,
                                       ErrorMessageConst.C_BookInfoUnacquiredErrorCaprion,
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Asterisk);
                return;
            }
            // 書籍情報を取得済みの場合
            // フォームを閉じる
            this.Close();
            return;
        }

        private void CancelButtonClick(object sender, EventArgs e) {
            
        }

        private void IsbnTextBox_KeyDown(object sender, KeyEventArgs e) {
            // クリップボード内に半角数字以外が含まれている場合、ペースト不可。
            if (e.KeyData == (Keys.Control | Keys.V)) {
                string wClipboardText = Clipboard.GetText();
                if (!Regex.IsMatch(wClipboardText, @"^[0-9]+$")) {
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void IsbnTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            // バックスペースは利用可
            if (e.KeyChar == '\b') {
                return;
            }

            // 半角数字以外の押下時はイベントをキャンセルする
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }
    }
}
