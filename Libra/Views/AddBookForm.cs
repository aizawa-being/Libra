using Libra.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra.Views {
    public partial class AddBookForm : Form {
        public AddBookForm() {
            InitializeComponent();
        }

        private void AddButtonClick(object sender, EventArgs e) {

        }

        private void CancelButtonClick(object sender, EventArgs e) {
            
        }

        private async void GetBookInfoButtonClickAsync(object sender, EventArgs e) {
            var addBookFormConrtoller = new AddBookFormController();
            if (await addBookFormConrtoller.SetAddBook(this.isbnTextBox.Text)) {
                this.titleLabel.Text = addBookFormConrtoller.BookToAdd.Title;
                this.authorLabel.Text = addBookFormConrtoller.BookToAdd.Author;
            }
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
