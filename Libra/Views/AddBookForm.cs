using Libra.Controls;
using Libra.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Libra.Views {
    public partial class AddBookForm : Form {
        private readonly IAddBookController F_AddBookController;

        public AddBookForm() {
            this.F_AddBookController = new AddBookFormController();
            InitializeComponent();
        }

        public AddBookForm(IAddBookController vAddBookController) {
            this.F_AddBookController = vAddBookController;
            InitializeComponent();
        }

        private async void GetBookInfoButtonClickAsync(object sender, EventArgs e) {
            await this.F_AddBookController.SetAddBook(this.isbnTextBox.Text);
            if (this.F_AddBookController.ExistAddBook()) {
                // 書籍情報取得成功時
                var book = this.F_AddBookController.GetAddBook();
                this.titleLabel.Text = book.Title;
                this.authorLabel.Text = book.Author;
            } else {
                // 書籍情報取得失敗時
                this.titleLabel.Text = "";
                this.authorLabel.Text = "";
            }
        }

        private void AddButtonClick(object sender, EventArgs e) {
            if (this.F_AddBookController.ExistAddBook()) {
                // 書籍情報を取得済み
                var addBookId = this.F_AddBookController.RegisterAddBook();
                // フォームを閉じる
                this.OnClosed(e);
                return;
            }
            // 書籍情報が未取得の場合、書籍追加は行わない。
            IMessageBoxService messageBoxService = new MessageBoxService();
            messageBoxService.Show(ErrorMessageConst.BookInfoUnacquiredError,
                                   ErrorMessageConst.BookInfoUnacquiredErrorCaprion,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Asterisk);
            return;
        }

        private void CancelButtonClick(object sender, EventArgs e) {
            
        }

        private void IsbnTextBox_KeyDown(object sender, KeyEventArgs e) {
            // クリップボード内に半角数字以外が含まれている場合、ペースト不可。
            if (e.KeyData == (Keys.Control | Keys.V)) {
                string clipboardText = Clipboard.GetText();
                if (!Regex.IsMatch(clipboardText, @"^[0-9]+$")) {
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
