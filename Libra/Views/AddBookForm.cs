using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Libra {
    public partial class AddBookForm : Form {
        /// <summary>
        /// 追加した書籍のID
        /// 書籍を追加していない場合の初期値は-1
        /// </summary>
        public int AddBookId { get; private set; } = -1;
        private readonly IAddBookControl FAddBookControl;

        public AddBookForm() {
            this.FAddBookControl = new AddBookControl();
            InitializeComponent();
        }

        public AddBookForm(IAddBookControl vAddBookControl) {
            this.FAddBookControl = vAddBookControl;
            InitializeComponent();
        }

        private async void GetBookInfoButtonClickAsync(object sender, EventArgs e) {
            await this.FAddBookControl.SetAddBook(this.isbnTextBox.Text);
            if (this.FAddBookControl.ExistAddBook()) {
                // 書籍情報取得成功時
                var wBook = this.FAddBookControl.GetAddBook();
                this.titleLabel.Text = wBook.Title;
                this.authorLabel.Text = wBook.Author;
            } else {
                // 書籍情報取得失敗時
                this.titleLabel.Text = "";
                this.authorLabel.Text = "";
            }
        }

        private void AddButtonClick(object sender, EventArgs e) {
            this.addButton.Focus();
            var wAddBook = this.FAddBookControl.GetAddBook();
            if (this.FAddBookControl.TryRegisterAddBook(wAddBook, out int vBookId)) {
                // 書籍追加に成功した場合
                this.AddBookId = vBookId;
                // フォームを閉じる
                this.Close();
            }
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
