using Libra.Controls;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Libra.Views {
    public partial class AddBookForm : Form {
        public AddBookForm() {
            InitializeComponent();
        }

        private async void GetBookInfoButtonClickAsync(object sender, EventArgs e) {
            IAddBookController addBookController = new AddBookFormController();
            await addBookController.SetAddBook(this.isbnTextBox.Text);
            var book = addBookController.GetAddBook();
            if (book == null) {
                // 書籍情報取得失敗時
                this.titleLabel.Text = "";
                this.authorLabel.Text = "";
            } else {
                // 書籍情報取得成功時
                this.titleLabel.Text = book.Title;
                this.authorLabel.Text = book.Author;
            }
        }

        private void AddButtonClick(object sender, EventArgs e) {

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
